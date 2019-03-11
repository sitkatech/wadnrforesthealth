//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Authenticator]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public abstract partial class Authenticator : IHavePrimaryKey
    {
        public static readonly AuthenticatorADFS ADFS = AuthenticatorADFS.Instance;
        public static readonly AuthenticatorSAW SAW = AuthenticatorSAW.Instance;

        public static readonly List<Authenticator> All;
        public static readonly ReadOnlyDictionary<int, Authenticator> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static Authenticator()
        {
            All = new List<Authenticator> { ADFS, SAW };
            AllLookupDictionary = new ReadOnlyDictionary<int, Authenticator>(All.ToDictionary(x => x.AuthenticatorID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected Authenticator(int authenticatorID, string authenticatorName, string authenticatorFullName)
        {
            AuthenticatorID = authenticatorID;
            AuthenticatorName = authenticatorName;
            AuthenticatorFullName = authenticatorFullName;
        }

        [Key]
        public int AuthenticatorID { get; private set; }
        public string AuthenticatorName { get; private set; }
        public string AuthenticatorFullName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return AuthenticatorID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(Authenticator other)
        {
            if (other == null)
            {
                return false;
            }
            return other.AuthenticatorID == AuthenticatorID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Authenticator);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return AuthenticatorID;
        }

        public static bool operator ==(Authenticator left, Authenticator right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Authenticator left, Authenticator right)
        {
            return !Equals(left, right);
        }

        public AuthenticatorEnum ToEnum { get { return (AuthenticatorEnum)GetHashCode(); } }

        public static Authenticator ToType(int enumValue)
        {
            return ToType((AuthenticatorEnum)enumValue);
        }

        public static Authenticator ToType(AuthenticatorEnum enumValue)
        {
            switch (enumValue)
            {
                case AuthenticatorEnum.ADFS:
                    return ADFS;
                case AuthenticatorEnum.SAW:
                    return SAW;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum AuthenticatorEnum
    {
        ADFS = 1,
        SAW = 2
    }

    public partial class AuthenticatorADFS : Authenticator
    {
        private AuthenticatorADFS(int authenticatorID, string authenticatorName, string authenticatorFullName) : base(authenticatorID, authenticatorName, authenticatorFullName) {}
        public static readonly AuthenticatorADFS Instance = new AuthenticatorADFS(1, @"ADFS", @"Washington DNR ADFS Account");
    }

    public partial class AuthenticatorSAW : Authenticator
    {
        private AuthenticatorSAW(int authenticatorID, string authenticatorName, string authenticatorFullName) : base(authenticatorID, authenticatorName, authenticatorFullName) {}
        public static readonly AuthenticatorSAW Instance = new AuthenticatorSAW(2, @"SAW", @"Secure Access Washington Account");
    }
}