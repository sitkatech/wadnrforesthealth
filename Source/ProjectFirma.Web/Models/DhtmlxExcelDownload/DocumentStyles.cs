using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class DocumentStyles
    {
        private WorkbookPart _wpart;
        protected List<string> fontsXML = new List<string>();
        protected List<string> formatsXML = new List<string>();
        protected List<string> fillsXML = new List<string>();

        public DocumentStyles(WorkbookPart wpart)
        {
            _wpart = wpart;
        }

        public void Save()
        {
            _wpart.WorkbookStylesPart.Stylesheet.Save();
        }

        public NumberingFormat GetNumberingFormat(uint numFmtId)
        {
            Stylesheet ss = _wpart.WorkbookStylesPart.Stylesheet;
            if (ss.NumberingFormats == null)
            {
                ss.NumberingFormats = new NumberingFormats();
                ss.Save();
            }
            var existing = (from x in ss.NumberingFormats.Elements<NumberingFormat>()
                            where x.NumberFormatId == numFmtId
                            select x).FirstOrDefault();
            return existing;
        }

        public uint EnsureCustomNumberingFormat(NumberingFormat nfNew)
        {
            Stylesheet ss = EnsureStylesheet();
            if (ss.NumberingFormats == null)
            {
                ss.NumberingFormats = new NumberingFormats();
                ss.Save();
            }
            var existing = (from nf in ss.NumberingFormats.Elements<NumberingFormat>()
                            where nf.FormatCode == nfNew.FormatCode
                            select nf).FirstOrDefault();

            if (existing == null)
            {
                uint existingMaxNumFmtId = (from nf in ss.NumberingFormats.Elements<NumberingFormat>()
                                            let id = (uint)(nf.NumberFormatId ?? 0)
                                            select id).FirstOrDefault();
                var newNumFmtId = Math.Max(128, existingMaxNumFmtId + 1);
                nfNew.NumberFormatId = newNumFmtId;
                ss.NumberingFormats.Append(nfNew);
                ss.NumberingFormats.Count = (uint)ss.NumberingFormats.Count();
                ss.Save();
            }
            return nfNew.NumberFormatId;
        }

        public Font GetFont(uint idx)
        {
            Stylesheet ss = _wpart.WorkbookStylesPart.Stylesheet;
            if (ss != null && ss.Fonts != null)
                return ss.Fonts.Elements<Font>().ElementAt((int)idx);
            return null;
        }

        public Font MergeFont(Font fontNew, Font fontTarget)
        {
            if (fontNew.FontCharSet != null)
                fontTarget.FontCharSet.Val = fontNew.FontCharSet.Val;
            if (fontNew.FontFamilyNumbering != null)
                fontTarget.FontFamilyNumbering.Val = fontNew.FontFamilyNumbering.Val;
            if (fontNew.FontName != null)
                fontTarget.FontName.Val = fontNew.FontName.Val;
            if (fontNew.FontScheme != null)
                fontTarget.FontScheme.Val = fontNew.FontScheme.Val;
            if (fontNew.FontSize != null)
                fontTarget.FontSize.Val = fontNew.FontSize.Val;
            if (fontNew.Bold != null)
            {
                if (fontNew.Bold.Val == "0")
                    fontTarget.Bold = null;
                else
                {
                    fontTarget.Bold = fontTarget.Bold ?? new Bold();
                    fontTarget.Bold.Val = fontNew.Bold.Val;
                }
            }
            else
            {
                fontTarget.Bold = null;
            }
            if (fontNew.Italic != null)
            {
                if (fontNew.Italic.Val == "0")
                    fontTarget.Italic = null;
                else
                {
                    fontTarget.Italic = fontTarget.Italic ?? new Italic();
                    fontTarget.Italic.Val = fontNew.Italic.Val;
                }
            }
            return fontTarget;
        }

        protected bool compareFont(Font fNew, Font fBase)
        {
            //   fNew.VerticalTextAlignment.Val = TextVerticalAlignmentValues.

            return GenericElementCompare(fNew, fBase);
        }

        public uint MergeAndRegisterFont(Font fNew, UInt32Value baseFontsIdx, bool doSave)
        {
            Stylesheet ss = EnsureStylesheet();
            uint ret = MergeAndRegisterStyleElement<Font, Fonts>(fNew, ss.Fonts, MergeFont, compareFont, baseFontsIdx, doSave);
            if (ss.Fonts.Count != (uint)ss.Fonts.Count())
            {
                ss.Fonts.Count = (uint)ss.Fonts.Count();
                if (doSave)
                    ss.Save();
            }
            return ret;
        }

        public Border GetBorder(uint idx)
        {
            Stylesheet ss = _wpart.WorkbookStylesPart.Stylesheet;
            return ss.Borders.Elements<Border>().ElementAt((int)idx);
        }
        public Border borderCombine(Border elemNew, Border elemBase)
        {
            if (elemNew.TopBorder != null)
                elemBase.TopBorder = (TopBorder)elemNew.TopBorder.CloneNode(true);
            if (elemNew.BottomBorder != null)
                elemBase.BottomBorder = (BottomBorder)elemNew.BottomBorder.CloneNode(true);
            if (elemNew.LeftBorder != null)
                elemBase.LeftBorder = (LeftBorder)elemNew.LeftBorder.CloneNode(true);
            if (elemNew.RightBorder != null)
                elemBase.RightBorder = (RightBorder)elemNew.RightBorder.CloneNode(true);
            if (elemNew.DiagonalBorder != null)
                elemBase.DiagonalBorder = (DiagonalBorder)elemNew.DiagonalBorder.CloneNode(true);
            return elemBase;
        }



        protected bool compareBorder(Border bNew, Border bOld)
        {
            return GenericElementCompare(bNew, bOld);

        }
        int b_count = -1;//ugly, i know...
        //there is only 1 custom type of border, so dont need to realy compare them,
        // first one - will be the default one, second - custom
        protected bool compareBorderFake(Border bNew, Border bOld)
        {
            if (b_count <= 0)
            {
                b_count++;
                return false;
            }
            else
            {
                b_count = 0;
                return true;
            }


        }

        public uint MergeAndRegisterBorder(Border bNew, UInt32Value baseBordersIdx, bool doSave)
        {

            Stylesheet ss = EnsureStylesheet();

            uint ret;
            if (baseBordersIdx == "0")
            {
                ret = MergeAndRegisterStyleElement<Border, Borders>(bNew, ss.Borders,
                    borderCombine, compareBorderFake, baseBordersIdx, doSave);
            }
            else
            {
                ret = MergeAndRegisterStyleElement<Border, Borders>(bNew, ss.Borders,
                    borderCombine, compareBorder, baseBordersIdx, doSave);
            }


            if (ss.Borders.Count != (uint)ss.Borders.Count())
            {
                ss.Borders.Count = (uint)ss.Borders.Count();
                if (doSave)
                    ss.Save();
            }
            return ret;
        }

        public Fill GetFill(uint idx)
        {
            Stylesheet ss = _wpart.WorkbookStylesPart.Stylesheet;
            return ss.Fills.Elements<Fill>().ElementAt((int)idx);
        }


        protected Fill fillCombine(Fill elemNew, Fill elemBase)
        {

            // Appears that Fill object clears GradientFill when PatternFill is set and vice-versa
            if (elemNew.PatternFill != null)
            {
                elemBase.PatternFill = (PatternFill)elemNew.PatternFill.CloneNode(true);
            }
            else if (elemNew.GradientFill != null)
            {
                elemBase.GradientFill = (GradientFill)elemNew.GradientFill.CloneNode(true);
            }
            return elemBase;
        }

        protected bool fillCompare(Fill fill1, Fill fill2)
        {
            bool match = true;

            if (fill1.InnerXml != fill2.InnerXml)
                match = false;
            return match;
        }

        public uint MergeAndRegisterFill(Fill fNew, UInt32Value baseFillsIdx, bool doSave)
        {


            Stylesheet ss = EnsureStylesheet();
            uint ret = MergeAndRegisterStyleElement<Fill, Fills>(fNew, ss.Fills,
                fillCombine, fillCompare, baseFillsIdx, doSave);
            if (ss.Fills.Count != (uint)ss.Fills.Count())
            {
                ss.Fills.Count = (uint)ss.Fills.Count();
                if (doSave)
                    ss.Save();
            }
            return ret;
        }

        public CellFormat GetCellFormat(uint idx)
        {
            Stylesheet ss = _wpart.WorkbookStylesPart.Stylesheet;
            return ss.CellFormats.Elements<CellFormat>().ElementAt((int)idx);
        }

        protected CellFormat formatCombine(CellFormat elemNew, CellFormat elemBase)
        {
            if (elemNew.ApplyNumberFormat != null && elemNew.ApplyNumberFormat.Value)
            {
                elemBase.NumberFormatId = elemNew.NumberFormatId;
                elemBase.ApplyNumberFormat = elemNew.ApplyNumberFormat;
            }

            if (elemNew.ApplyFont != null && elemNew.ApplyFont.Value)
            {
                elemBase.FontId = elemNew.FontId;
                elemBase.ApplyFont = elemNew.ApplyFont;
            }

            if (elemNew.ApplyBorder != null && elemNew.ApplyBorder.Value)
            {
                elemBase.BorderId = elemNew.BorderId;
                elemBase.ApplyBorder = elemNew.ApplyBorder;
            }

            if (elemNew.ApplyFill != null && elemNew.ApplyFill.Value)
            {
                elemBase.FillId = elemNew.FillId;
                elemBase.ApplyFill = elemNew.ApplyFill;
            }
            if (elemNew.FormatId != null)
                elemBase.FormatId = elemNew.FormatId;
            else
                elemBase.FormatId = null;

            return elemBase;
        }

        protected bool formatCompare(CellFormat cfToTest, CellFormat cfExisting)
        {
            bool match = true;

            if ((cfToTest.NumberFormatId != cfExisting.NumberFormatId && (cfToTest.NumberFormatId == null || cfExisting.NumberFormatId == null)) ||
                ((cfToTest.NumberFormatId != null && cfExisting.NumberFormatId != null) && (cfToTest.NumberFormatId.InnerText != cfExisting.NumberFormatId.InnerText)))
                match = false;
            if ((cfToTest.FillId != cfExisting.FillId && (cfToTest.FillId == null || cfExisting.FillId == null)) ||
                ((cfToTest.FillId != null && cfExisting.FillId != null) && (cfToTest.FillId.InnerText != cfExisting.FillId.InnerText)))
                match = false;
            if ((cfToTest.BorderId != cfExisting.BorderId && (cfToTest.BorderId == null || cfExisting.BorderId == null)) ||
                ((cfToTest.BorderId != null && cfExisting.BorderId != null) && (cfToTest.BorderId.InnerText != cfExisting.BorderId.InnerText)))
                match = false;
            if ((cfToTest.FontId != cfExisting.FontId && (cfToTest.FontId == null || cfExisting.FontId == null)) ||
                ((cfToTest.FontId != null && cfExisting.FontId != null) && (cfToTest.FontId.InnerText != cfExisting.FontId.InnerText)))
                match = false;
            if ((cfToTest.FormatId != cfExisting.FormatId && (cfToTest.FormatId == null || cfExisting.FormatId == null)) ||
                ((cfToTest.FormatId != null && cfExisting.FormatId != null) && (cfToTest.FormatId.InnerText != cfExisting.FormatId.InnerText)))
                match = false;
            return match;
        }

        public uint MergeAndRegisterCellFormat(CellFormat cfNew, UInt32Value baseCellXfsIdx, bool doSave)
        {
            if (cfNew.NumberFormatId == null)
                cfNew.NumberFormatId = 0;


            // ww

            Stylesheet ss = EnsureStylesheet();
            uint ret = MergeAndRegisterStyleElement<CellFormat, CellFormats>(cfNew, ss.CellFormats,
                formatCombine, formatCompare, baseCellXfsIdx, doSave);
            if (ss.CellFormats.Count != (uint)ss.CellFormats.Count())
            {
                ss.CellFormats.Count = (uint)ss.CellFormats.Count();
                if (doSave)
                    ss.Save();
            }
            return ret;
        }

        /// <summary>
        /// Check if date format is one of the built-in date format
        /// http://social.msdn.microsoft.com/Forums/en-US/oxmlsdk/thread/3143212a-c798-4a93-ab2b-f08625c5cbe5/
        /// http://social.msdn.microsoft.com/Forums/en-US/oxmlsdk/thread/e27aaf16-b900-4654-8210-83c5774a179c/
        /// http://www.documentinteropinitiative.com/implnotes/ISO-IEC29500-2008/001.018.008.030.000.000.000.aspx
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public bool IsDateFormat(CellFormat cf)
        {
            return cf.NumberFormatId >= 14 && cf.NumberFormatId <= 22;
        }

        public Stylesheet EnsureStylesheet()
        {
            WorkbookPart wpart = _wpart;
            if (wpart.WorkbookStylesPart == null)
            {
                wpart.AddNewPart<WorkbookStylesPart>();
                Stylesheet ss = new Stylesheet();
                wpart.WorkbookStylesPart.Stylesheet = ss;

                Font fontDefault = new Font()
                {
                    FontSize = new FontSize() { Val = 11 },
                    Color = new Color() { Theme = 1 },
                    FontName = new FontName() { Val = "Calibri" },
                    FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
                    FontScheme = new FontScheme() { Val = FontSchemeValues.Minor }
                };
                Fill fillDefault = new Fill()
                {
                    PatternFill = new PatternFill() { PatternType = PatternValues.None }
                };
                Fill fillDefault2 = new Fill()
                {
                    PatternFill = new PatternFill() { PatternType = PatternValues.Gray125 }
                };
                Border borderDefault = new Border()
                {
                    LeftBorder = new LeftBorder(),
                    RightBorder = new RightBorder(),
                    TopBorder = new TopBorder(),
                    BottomBorder = new BottomBorder(),
                    DiagonalBorder = new DiagonalBorder()
                };
                CellFormat xfCellStyleDefault = new CellFormat()
                {
                    NumberFormatId = 0,
                    BorderId = 0,
                    FontId = 0,
                    FillId = 0,
                };
                CellFormat xfCellDefault = new CellFormat()
                {
                    NumberFormatId = 0,
                    FontId = 0,
                    BorderId = 0,
                    FillId = 0,
                    FormatId = 0
                };
                CellStyle defaultCellStyle = new CellStyle()
                {
                    Name = "Normal",
                    BuiltinId = 0,
                    FormatId = 0
                };

                ss.Fonts = new Fonts() { Count = 1 };
                ss.Fonts.Append(fontDefault);
                ss.Fills = new Fills() { Count = 2 };
                ss.Fills.Append(fillDefault);
                ss.Fills.Append(fillDefault2);
                ss.Borders = new Borders() { Count = 1 };
                ss.Borders.Append(borderDefault);
                ss.CellStyleFormats = new CellStyleFormats() { Count = 1 };
                ss.CellStyleFormats.Append(xfCellStyleDefault);
                ss.CellFormats = new CellFormats() { Count = 1 };
                ss.CellFormats.Append(xfCellDefault);
                ss.CellStyles = new CellStyles() { Count = 1 };
                ss.CellStyles.Append(defaultCellStyle);
                ss.DifferentialFormats = new DifferentialFormats();
                ss.TableStyles = new TableStyles() { Count = 0 };
                //ss.Save();
            }
            return wpart.WorkbookStylesPart.Stylesheet;
        }



        protected string getFormatHash(CellFormat format)
        {
            return string.Concat(format.NumberFormatId) + "|" +
                string.Concat(format.FillId) + "|" +
                string.Concat(format.BorderId) + "|" +
                string.Concat(format.FontId) + "|" +
                string.Concat(format.FormatId);
        }
        protected string getFontHash(Font fnt)
        {
            return fnt.InnerXml;
        }
        protected string getFillHash(Fill fill)
        {
            return fill.InnerXml;
        }

        private uint MergeAndRegisterStyleElement<TElement, TParent>(TElement elemNew, TParent parent,
                                                 Func<TElement, TElement, TElement> fnCombine,
                                                 Func<TElement, TElement, bool> fnCompare,
                                                 UInt32Value baseElementIdx, bool doSave)
            where TElement : OpenXmlElement
            where TParent : OpenXmlCompositeElement
        {
            int elemIdxMatch = -1;
            TElement elemCombined = null;

            if (baseElementIdx != null)
            {
                elemCombined = (TElement)parent.Elements<TElement>().ElementAt((int)baseElementIdx.Value).Clone();
                elemCombined = fnCombine(elemNew, elemCombined);
            }
            else
            {
                elemCombined = elemNew;
            }

            int ctr = 0;

            //speed up for fonts
            bool isFont = elemCombined as Font != null;
            bool isFormat = elemCombined as CellFormat != null;
            bool isFill = elemCombined as Fill != null;
            string xml = "";
            if (isFont || isFormat || isFill)
            {
                if (isFont)
                    xml = getFontHash(elemCombined as Font);
                else if (isFormat)
                    xml = getFormatHash(elemCombined as CellFormat);
                else
                    xml = getFillHash(elemCombined as Fill);

            }
            List<string> itemsHashContainer = new List<string>();
            if (isFont)
                itemsHashContainer = fontsXML;
            else if (isFormat)
                itemsHashContainer = formatsXML;
            else
                itemsHashContainer = fillsXML;

            foreach (TElement e in parent.Elements<TElement>())
            {
                if (!isFont && !isFormat)
                {
                    if (fnCompare(e, elemCombined))
                    {
                        elemIdxMatch = ctr;
                        break;
                    }
                }
                else
                {


                    if (itemsHashContainer.Count <= ctr)
                    {
                        if (isFont)
                            itemsHashContainer.Add(getFontHash(e as Font));
                        else if (isFormat)
                            itemsHashContainer.Add(getFormatHash(e as CellFormat));
                        else
                            itemsHashContainer.Add(getFillHash(e as Fill));
                    }
                    if (xml.Equals(itemsHashContainer[ctr]))
                    {
                        elemIdxMatch = ctr;
                        break;
                    }
                }
                ctr++;
            }
            if (elemIdxMatch == -1)
            {
                //speed up font comparing
                if (isFont || isFormat || isFill)
                {
                    itemsHashContainer.Add(xml);
                }
                parent.Append(elemCombined);
                if (doSave)
                    EnsureStylesheet().Save();
                elemIdxMatch = (int)(parent.ChildElements.Count - 1);
            }
            return (uint)elemIdxMatch;
        }

        private bool GenericElementCompare(OpenXmlElement e1, OpenXmlElement e2)
        {
            return e1.InnerXml.Equals(e2.InnerXml);
        }
    }
}