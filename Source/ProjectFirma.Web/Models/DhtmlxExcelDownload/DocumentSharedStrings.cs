using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    internal class DocumentSharedStrings
    {
        private WorkbookPart _wpart;
        private SharedStringTablePart _ssPart;
        private SortedList<uint, string> _stringCache;
        private Dictionary<string, uint> _indexLookup;
        private bool _changed = false;

        public DocumentSharedStrings(WorkbookPart wpart)
        {
            _wpart = wpart;
            _indexLookup = new Dictionary<string, uint>();

            if ((_ssPart = _wpart.SharedStringTablePart) != null)
            {
                SharedStringTable ssTable = _ssPart.SharedStringTable;
                uint idx = 0;
                foreach (var sharedStr in ssTable.Elements<SharedStringItem>())
                {
                    string valueStr = sharedStr.Text.Text;
                    _indexLookup[valueStr] = idx;
                    idx++;
                }
            }
        }

        public string Get(uint idx)
        {
            // int -> string lookup is not created until first lookup
            // this makes writing faster
            StringCacheLazyInit();
            return _stringCache[idx];
        }

        public int Put(string valueStr)
        {
            {
                uint existingIdx = 0;
                if (_indexLookup.TryGetValue(valueStr, out existingIdx))
                    return (int)existingIdx;
            }

            uint sharedStrIdx = (uint)_indexLookup.Count;
            if (_stringCache != null)
                _stringCache[sharedStrIdx] = valueStr;
            _indexLookup[valueStr] = sharedStrIdx;
            _changed = true;

            return (int)sharedStrIdx;
        }

        public void Save()
        {
            if (!_changed)
                return;

            if (_ssPart != null)
            {
                string originalSSPartId = _wpart.GetIdOfPart(_ssPart);
                _wpart.DeletePart(originalSSPartId);
            }

            SharedStringTablePart newSSPart = _wpart.AddNewPart<SharedStringTablePart>();
            using (OpenXmlWriter writer = OpenXmlWriter.Create(newSSPart))
            {
                writer.WriteStartElement(new SharedStringTable());

                if (_stringCache == null)
                {
                    string[] outputList = new string[_indexLookup.Count];
                    foreach (var i in _indexLookup)
                        outputList[i.Value] = i.Key;
                    for (uint idx = 0; idx < outputList.Length; idx++)
                    {
                        writer.WriteStartElement(new SharedStringItem());
                        writer.WriteElement(new Text(outputList[idx]));
                        writer.WriteEndElement();
                    }
                }
                else
                {
                    foreach (var i in _stringCache)
                    {
                        writer.WriteStartElement(new SharedStringItem());
                        writer.WriteElement(new Text(i.Value));
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
        }

        private SharedStringTablePart EnsureSharedStringTablePart()
        {
            if (_ssPart == null)
            {
                _ssPart = _wpart.AddNewPart<SharedStringTablePart>();
                _ssPart.SharedStringTable = new SharedStringTable();
                _ssPart.SharedStringTable.Save();
            }
            return _ssPart;
        }

        private void StringCacheLazyInit()
        {
            if (_stringCache == null)
            {
                _stringCache = new SortedList<uint, string>();
                foreach (var i in _indexLookup)
                {
                    _stringCache[i.Value] = i.Key;
                }
            }
        }
    }
}