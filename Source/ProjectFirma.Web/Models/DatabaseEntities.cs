﻿/*-----------------------------------------------------------------------
<copyright file="DatabaseEntities.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Transactions;
using ProjectFirma.Web.Common;
using SitkaController = ProjectFirma.Web.Common.SitkaController;

namespace ProjectFirma.Web.Models
{
    public partial class DatabaseEntities : SitkaController.ISitkaDbContext
    {

        public Database GetDatabase()
        {
            return this.GetDbContext().Database;
        }

        public DatabaseEntities(string connectionString)
            : base("name=DatabaseEntities")
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        public int SaveChanges(IPrincipal userPrincipal)
        {
            var person = HttpRequestStorage.Person;
            return SaveChanges(person);
        }

        public int SaveChanges(Person userPerson)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.Snapshot }))
            {
                return SaveChangesImpl(userPerson, scope);
            }
        }

        public int SaveChanges(Person userPerson, IsolationLevel isolationLevel)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = isolationLevel }))
            {
                return SaveChangesImpl(userPerson, scope);
            }
        }

        /// <summary>
        /// Save changes using the current person
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var person = HttpRequestStorage.Person;
            return SaveChanges(person);
        }

        public int SaveChanges(IsolationLevel isolationLevel)
        {
            var person = HttpRequestStorage.Person;
            return SaveChanges(person, isolationLevel);
        }

        public int SaveChangesWithNoAuditing()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        public int SaveChangesWithNoAuditingInsertOnly()
        {
            return base.SaveChanges();
        }

        // private so I didn't have to write the warning above twice.

        private int SaveChangesImpl(Person person, TransactionScope scope)
        {
            // We have Configuration.AutoDetectChangesEnabled turned off by default instead of it being true out of the box
            // We only need to detect changes when we know we are saving
            ChangeTracker.DetectChanges();

            var dbEntityEntries = ChangeTracker.Entries().ToList();
            var addedEntries = dbEntityEntries.Where(e => e.State == EntityState.Added).ToList();
            var modifiedEntries = dbEntityEntries.Where(e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();
            var objectContext = GetObjectContext();

            foreach (var entry in modifiedEntries)
            {
                // For each changed record, get the audit record entries and add them
                var auditRecordsForChange = AuditLog.GetAuditLogRecordsForModifiedOrDeleted(entry, person, objectContext);
                AuditLogs.AddRange(auditRecordsForChange);
            }

            int changes;
            try
            {
                changes = base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            foreach (var entry in addedEntries)
            {
                // For each added record, get the audit record entries and add them
                var auditRecordsForChange = AuditLog.GetAuditLogRecordsForAdded(entry, person, objectContext);
                AuditLogs.AddRange(auditRecordsForChange);
            }
            // we need to save the audit log entries now
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException validationException)
            {
                Console.WriteLine(validationException);
                throw;
            }

            scope.Complete();
            return changes;
        }

        public DbContext GetDbContext()
        {
            return this;
        }

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter) this).ObjectContext;
        }
    }
}
