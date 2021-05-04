using System.Xml;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload.ExcelCreator
{
    public class Row
    {
        private Cell[] cells;

        public void parse(XmlNode parent)
        {
            XmlNodeList nodes = ((XmlElement)parent).GetElementsByTagName("cell");
            XmlNode text_node;
            if ((nodes != null) && (nodes.Count > 0))
            {
                cells = new Cell[nodes.Count];
                for (int i = 0; i < nodes.Count; i++)
                {
                    text_node = nodes[i];
                    Cell cell = new Cell();
                    if (text_node != null)
                        cell.Parse(text_node);
                    cells[i] = cell;
                }
            }
        }

        public Cell[] getCells()
        {
            return cells;
        }
    }
}