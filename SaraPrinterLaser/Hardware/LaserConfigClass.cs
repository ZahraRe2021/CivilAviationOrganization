using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SaraPrinterLaser.Hardware
{
    [Serializable]
    public class LaserConfigClass
    {
        public double Xcenter;
        public double Ycenter;
        public double XMargin;
        public double YMargin;
        public double Ratio;
        public double Brightness;
        public double Contrast;
        public double PointTime;
        public int dpi;
        public int minLowGrayPt;
        public int bmpScanAttr;
        public int bmpttrib;
        public double RotateAngle;

        public bool blDisableMarkLowGray { get; set; }
        public bool blInvert { get; set; }
        public bool blGray { get; set; }
        public bool blDither { get; set; }
        public bool blBidirectional { get; set; }
        public bool blYscan { get; set; }
        public bool blPower { get; set; }
        public bool blOffSetPT { get; set; }
        public bool blOptimize { get; set; }
        public bool blDynamic { get; set; }
        public bool blDPIfixedWidth { get; set; }
        public bool blDPIfixedHeight { get; set; }
        public bool blBrightness { get; set; }
        public bool blDrill { get; set; }
        public bool blfixDPI { get; set; }



        public static void Save(LaserConfigClass Newconfig)
        {

            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlDoc.CreateElement("SaraHardwareCompanyLaserPrinterSetting");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);
            XmlElement parentNode = xmlDoc.CreateElement("AllObjectParent");

            string strXcenter = Newconfig.Xcenter.ToString();
            string strYcenter = Newconfig.Ycenter.ToString();
            string strXmargin = Newconfig.XMargin.ToString();
            string strYmargin = Newconfig.YMargin.ToString();
            string strRatio = Newconfig.Ratio.ToString();
            string strBrightness = Newconfig.Brightness.ToString();
            string strContrast = Newconfig.Contrast.ToString();
            string strPointTime = Newconfig.PointTime.ToString();
            string strdpi = Newconfig.dpi.ToString();
            string strminLowGrayPt = Newconfig.minLowGrayPt.ToString();
            string strbmpScanAttr = Newconfig.bmpScanAttr.ToString();
            string strbmpttrib = Newconfig.bmpttrib.ToString();
            string strRotateAngle = Newconfig.RotateAngle.ToString();

            string strblDisableMarkLowGray = Newconfig.blDisableMarkLowGray.ToString();
            string strblInvert = Newconfig.blInvert.ToString();
            string strblGray = Newconfig.blGray.ToString();
            string strblDither = Newconfig.blDither.ToString();
            string strblBidirectional = Newconfig.blBidirectional.ToString();
            string strblYscan = Newconfig.blYscan.ToString();
            string strblPower = Newconfig.blPower.ToString();
            string strblOffSetPT = Newconfig.blOffSetPT.ToString();
            string strblOptimize = Newconfig.blOptimize.ToString();
            string strblDynamic = Newconfig.blDynamic.ToString();
            string strblDPIfixedWidth = Newconfig.blDPIfixedWidth.ToString();
            string strblDPIfixedHeight = Newconfig.blDPIfixedHeight.ToString();
            string strblBrightness = Newconfig.blBrightness.ToString();
            string strblDrill = Newconfig.blDrill.ToString();
            string strblfixDPI = Newconfig.blfixDPI.ToString();

            parentNode.SetAttribute("LaserPrinter", "Setting");
            xmlDoc.DocumentElement.PrependChild(parentNode);

            XmlElement SettingstrXcenter = xmlDoc.CreateElement("XPositionValue");
            XmlElement SettingstrYcenter = xmlDoc.CreateElement("YPositionValue");
            XmlElement SettingstrXMargin = xmlDoc.CreateElement("XMarginValue");
            XmlElement SettingstrYMargin = xmlDoc.CreateElement("YMarginValue");
            XmlElement SettingstrRatio = xmlDoc.CreateElement("ScaleValue");
            XmlElement SettingstrBrightness = xmlDoc.CreateElement("BrightnessValue");
            XmlElement SettingstrContrast = xmlDoc.CreateElement("ContrastValue");
            XmlElement SettingstrPointTime = xmlDoc.CreateElement("PointTimeValue");
            XmlElement Settingstrdpi = xmlDoc.CreateElement("DpiValue");
            XmlElement SettingstrminLowGrayPt = xmlDoc.CreateElement("DisableLowGrayPointValue");
            XmlElement SettingstrbmpScanAttr = xmlDoc.CreateElement("BmpScanAttribute");
            XmlElement Settingstrbmpttrib = xmlDoc.CreateElement("BmpAttribute");
            XmlElement SettingstrRotateAngle = xmlDoc.CreateElement("RotateAngle");

            XmlElement SettingstrblDisableMarkLowGray = xmlDoc.CreateElement("DisableLowGrayPointFlag");
            XmlElement SettingstrblInvert = xmlDoc.CreateElement("InvertFlag");
            XmlElement SettingstrblGray = xmlDoc.CreateElement("GrayFlag");
            XmlElement SettingstrblDither = xmlDoc.CreateElement("DitherFlag");
            XmlElement SettingstrblBidirectional = xmlDoc.CreateElement("BidirectionalFlag");
            XmlElement SettingstrblYscan = xmlDoc.CreateElement("YscanFlag");
            XmlElement SettingstrblPower = xmlDoc.CreateElement("PowerFlag");
            XmlElement SettingstrblOffSetPT = xmlDoc.CreateElement("OffsetFlag");
            XmlElement SettingstrblOptimize = xmlDoc.CreateElement("OptimizeFlag");
            XmlElement SettingstrblDynamic = xmlDoc.CreateElement("DynamicFlag");
            XmlElement SettingstrblDPIfixedWidth = xmlDoc.CreateElement("FixedDpiOnWidth");
            XmlElement SettingstrblDPIfixedHeight = xmlDoc.CreateElement("FixedDpiOnHeight");
            XmlElement SettingstrblBrightness = xmlDoc.CreateElement("BrightnessFlag");
            XmlElement SettingstrblDrill = xmlDoc.CreateElement("DrillModeFlag");
            XmlElement SettingstrblfixDPI = xmlDoc.CreateElement("FixDpiFlag");

            XmlText XmlTextstrXcenter = xmlDoc.CreateTextNode(strXcenter);
            XmlText XmlTextstrYcenter = xmlDoc.CreateTextNode(strYcenter);
            XmlText XmlTextstrXmargin = xmlDoc.CreateTextNode(strXmargin);
            XmlText XmlTextstrYmargin = xmlDoc.CreateTextNode(strYmargin);
            XmlText XmlTextstrRatio = xmlDoc.CreateTextNode(strRatio);
            XmlText XmlTextstrBrightness = xmlDoc.CreateTextNode(strBrightness);
            XmlText XmlTextstrContrast = xmlDoc.CreateTextNode(strContrast);
            XmlText XmlTextstrPointTime = xmlDoc.CreateTextNode(strPointTime);
            XmlText XmlTextstrdpi = xmlDoc.CreateTextNode(strdpi);
            XmlText XmlTextstrminLowGrayPt = xmlDoc.CreateTextNode(strminLowGrayPt);
            XmlText XmlTextstrbmpScanAttr = xmlDoc.CreateTextNode(strbmpScanAttr);
            XmlText XmlTextstrbmpttrib = xmlDoc.CreateTextNode(strbmpttrib);
            XmlText XmlTextstrRotateAngle = xmlDoc.CreateTextNode(strRotateAngle);

            XmlText XmlTextstrblDisableMarkLowGray = xmlDoc.CreateTextNode(strblDisableMarkLowGray);
            XmlText XmlTextstrblInvert = xmlDoc.CreateTextNode(strblInvert);
            XmlText XmlTextstrblGray = xmlDoc.CreateTextNode(strblGray);
            XmlText XmlTextstrblDither = xmlDoc.CreateTextNode(strblDither);
            XmlText XmlTextstrblBidirectional = xmlDoc.CreateTextNode(strblBidirectional);
            XmlText XmlTextstrblYscan = xmlDoc.CreateTextNode(strblYscan);
            XmlText XmlTextstrblPower = xmlDoc.CreateTextNode(strblPower);
            XmlText XmlTextstrblOffSetPT = xmlDoc.CreateTextNode(strblOffSetPT);
            XmlText XmlTextstrblOptimize = xmlDoc.CreateTextNode(strblOptimize);
            XmlText XmlTextstrblDynamic = xmlDoc.CreateTextNode(strblDynamic);
            XmlText XmlTextstrblDPIfixedWidth = xmlDoc.CreateTextNode(strblDPIfixedWidth);
            XmlText XmlTextstrblDPIfixedHeight = xmlDoc.CreateTextNode(strblDPIfixedHeight);
            XmlText XmlTextstrblBrightness = xmlDoc.CreateTextNode(strblBrightness);
            XmlText XmlTextstrblDrill = xmlDoc.CreateTextNode(strblDrill);
            XmlText XmlTextstrblfixDPI = xmlDoc.CreateTextNode(strblfixDPI);

            parentNode.AppendChild(SettingstrXcenter);
            parentNode.AppendChild(SettingstrYcenter);
            parentNode.AppendChild(SettingstrXMargin);
            parentNode.AppendChild(SettingstrYMargin);
            parentNode.AppendChild(SettingstrRatio);
            parentNode.AppendChild(SettingstrBrightness);
            parentNode.AppendChild(SettingstrContrast);
            parentNode.AppendChild(SettingstrPointTime);
            parentNode.AppendChild(Settingstrdpi);
            parentNode.AppendChild(SettingstrminLowGrayPt);
            parentNode.AppendChild(SettingstrbmpScanAttr);
            parentNode.AppendChild(Settingstrbmpttrib);
            parentNode.AppendChild(SettingstrRotateAngle);

            parentNode.AppendChild(SettingstrblDisableMarkLowGray);
            parentNode.AppendChild(SettingstrblInvert);
            parentNode.AppendChild(SettingstrblGray);
            parentNode.AppendChild(SettingstrblDither);
            parentNode.AppendChild(SettingstrblBidirectional);
            parentNode.AppendChild(SettingstrblYscan);
            parentNode.AppendChild(SettingstrblPower);
            parentNode.AppendChild(SettingstrblOffSetPT);
            parentNode.AppendChild(SettingstrblOptimize);
            parentNode.AppendChild(SettingstrblDynamic);
            parentNode.AppendChild(SettingstrblDPIfixedWidth);
            parentNode.AppendChild(SettingstrblDPIfixedHeight);
            parentNode.AppendChild(SettingstrblBrightness);
            parentNode.AppendChild(SettingstrblDrill);
            parentNode.AppendChild(SettingstrblfixDPI);




            SettingstrXcenter.AppendChild(XmlTextstrXcenter);
            SettingstrYcenter.AppendChild(XmlTextstrYcenter);
            SettingstrXMargin.AppendChild(XmlTextstrXmargin);
            SettingstrYMargin.AppendChild(XmlTextstrYmargin);
            SettingstrRatio.AppendChild(XmlTextstrRatio);
            SettingstrBrightness.AppendChild(XmlTextstrBrightness);
            SettingstrContrast.AppendChild(XmlTextstrContrast);
            SettingstrPointTime.AppendChild(XmlTextstrPointTime);
            Settingstrdpi.AppendChild(XmlTextstrdpi);
            SettingstrminLowGrayPt.AppendChild(XmlTextstrminLowGrayPt);
            SettingstrbmpScanAttr.AppendChild(XmlTextstrbmpScanAttr);
            Settingstrbmpttrib.AppendChild(XmlTextstrbmpttrib);
            SettingstrRotateAngle.AppendChild(XmlTextstrRotateAngle);


            SettingstrblDisableMarkLowGray.AppendChild(XmlTextstrblDisableMarkLowGray);
            SettingstrblInvert.AppendChild(XmlTextstrblInvert);
            SettingstrblGray.AppendChild(XmlTextstrblGray);
            SettingstrblDither.AppendChild(XmlTextstrblDither);
            SettingstrblBidirectional.AppendChild(XmlTextstrblBidirectional);
            SettingstrblYscan.AppendChild(XmlTextstrblYscan);
            SettingstrblPower.AppendChild(XmlTextstrblPower);
            SettingstrblOffSetPT.AppendChild(XmlTextstrblOffSetPT);
            SettingstrblOptimize.AppendChild(XmlTextstrblOptimize);
            SettingstrblDynamic.AppendChild(XmlTextstrblDynamic);
            SettingstrblDPIfixedWidth.AppendChild(XmlTextstrblDPIfixedWidth);
            SettingstrblDPIfixedHeight.AppendChild(XmlTextstrblDPIfixedHeight);
            SettingstrblBrightness.AppendChild(XmlTextstrblBrightness);
            SettingstrblDrill.AppendChild(XmlTextstrblDrill);
            SettingstrblfixDPI.AppendChild(XmlTextstrblfixDPI);

            xmlDoc.Save(@"Config\LaserSetting.xml");

        }
        public static LaserConfigClass load()
        {


            XmlDocument xmlDoc = new XmlDocument();
            XmlDocument xml = new XmlDocument();
            LaserConfigClass LaserSetting = new LaserConfigClass();
            try
            {

                xmlDoc.Load(@"Config\LaserSetting.xml");
                xml.LoadXml(xmlDoc.OuterXml);
                XmlNode xnList = xml.SelectSingleNode("SaraHardwareCompanyLaserPrinterSetting");

                string CntrlXPositionValue = "", CntrlXMarginValue = "", CntrlYMarginValue = "", CntrlYPositionValue = "", CntrlScaleValue = "", CntrlBrightnessValue = "",
             CntrlContrastValue = "", CntrlPointTimeValue = "", CntrlDpiValue = "", CntrlDisableLowGrayPointValue = "",
             CntrlBmpScanAttribute = "", CntrlBmpAttribute = "", CntrlRotateAngle = "",

             CntrlDisableLowGrayPointFlag = "", CntrlInvertFlag = "", CntrlGrayFlag = "", CntrlDitherFlag = "",
             CntrlBidirectionalFlag = "", CntrlYscanFlag = "", CntrlPowerFlag = "", CntrlOffsetFlag = "",
             CntrlOptimizeFlag = "", CntrlDynamicFlag = "", CntrlFixedDpiOnWidth = "", CntrlFixedDpiOnHeight = "",
             CntrlBrightnessFlag = "", CntrlDrillModeFlag = "", CntrlFixDpiFlag = "";

                foreach (XmlNode xn in xnList)
                {
                    CntrlXPositionValue = xn["XPositionValue"].InnerText;
                    CntrlYPositionValue = xn["YPositionValue"].InnerText;
                    CntrlXMarginValue = xn["XMarginValue"].InnerText;
                    CntrlYMarginValue = xn["YMarginValue"].InnerText;
                    CntrlScaleValue = xn["ScaleValue"].InnerText;
                    CntrlBrightnessValue = xn["BrightnessValue"].InnerText;
                    CntrlContrastValue = xn["ContrastValue"].InnerText;
                    CntrlPointTimeValue = xn["PointTimeValue"].InnerText;
                    CntrlDpiValue = xn["DpiValue"].InnerText;
                    CntrlDisableLowGrayPointValue = xn["DisableLowGrayPointValue"].InnerText;
                    CntrlBmpScanAttribute = xn["BmpScanAttribute"].InnerText;
                    CntrlBmpAttribute = xn["BmpAttribute"].InnerText;
                    CntrlRotateAngle = xn["RotateAngle"].InnerText;

                    CntrlDisableLowGrayPointFlag = xn["DisableLowGrayPointFlag"].InnerText;
                    CntrlInvertFlag = xn["InvertFlag"].InnerText;
                    CntrlGrayFlag = xn["GrayFlag"].InnerText;
                    CntrlDitherFlag = xn["DitherFlag"].InnerText;
                    CntrlBidirectionalFlag = xn["BidirectionalFlag"].InnerText;
                    CntrlYscanFlag = xn["YscanFlag"].InnerText;
                    CntrlPowerFlag = xn["PowerFlag"].InnerText;
                    CntrlOffsetFlag = xn["OffsetFlag"].InnerText;
                    CntrlOptimizeFlag = xn["OptimizeFlag"].InnerText;
                    CntrlDynamicFlag = xn["DynamicFlag"].InnerText;
                    CntrlFixedDpiOnWidth = xn["FixedDpiOnWidth"].InnerText;
                    CntrlFixedDpiOnHeight = xn["FixedDpiOnHeight"].InnerText;
                    CntrlBrightnessFlag = xn["BrightnessFlag"].InnerText;
                    CntrlDrillModeFlag = xn["DrillModeFlag"].InnerText;
                    CntrlFixDpiFlag = xn["FixDpiFlag"].InnerText;
                }

                LaserSetting.Xcenter = double.Parse(CntrlXPositionValue);
                LaserSetting.Ycenter = double.Parse(CntrlYPositionValue);
                LaserSetting.XMargin = double.Parse(CntrlXMarginValue);
                LaserSetting.YMargin = double.Parse(CntrlYMarginValue);
                LaserSetting.Ratio = double.Parse(CntrlScaleValue);
                LaserSetting.Brightness = double.Parse(CntrlBrightnessValue);
                LaserSetting.Contrast = double.Parse(CntrlContrastValue);
                LaserSetting.PointTime = double.Parse(CntrlPointTimeValue);
                LaserSetting.dpi = int.Parse(CntrlDpiValue);
                LaserSetting.minLowGrayPt = int.Parse(CntrlDisableLowGrayPointValue);
                LaserSetting.bmpScanAttr = int.Parse(CntrlBmpScanAttribute);
                LaserSetting.bmpttrib = int.Parse(CntrlBmpAttribute);
                LaserSetting.RotateAngle = double.Parse(CntrlRotateAngle);

                LaserSetting.blDisableMarkLowGray = bool.Parse(CntrlDisableLowGrayPointFlag);
                LaserSetting.blInvert = bool.Parse(CntrlInvertFlag);
                LaserSetting.blGray = bool.Parse(CntrlGrayFlag);
                LaserSetting.blDither = bool.Parse(CntrlDitherFlag);
                LaserSetting.blBidirectional = bool.Parse(CntrlBidirectionalFlag);
                LaserSetting.blYscan = bool.Parse(CntrlYscanFlag);
                LaserSetting.blPower = bool.Parse(CntrlPowerFlag);
                LaserSetting.blOffSetPT = bool.Parse(CntrlOffsetFlag);
                LaserSetting.blOptimize = bool.Parse(CntrlOptimizeFlag);
                LaserSetting.blDynamic = bool.Parse(CntrlDynamicFlag);
                LaserSetting.blDPIfixedWidth = bool.Parse(CntrlFixedDpiOnWidth);
                LaserSetting.blDPIfixedHeight = bool.Parse(CntrlFixedDpiOnHeight);
                LaserSetting.blBrightness = bool.Parse(CntrlBrightnessFlag);
                LaserSetting.blDrill = bool.Parse(CntrlDrillModeFlag);
                LaserSetting.blfixDPI = bool.Parse(CntrlFixDpiFlag);

            }
            catch (Exception)
            {


            }

            return LaserSetting;
        }

    }
}
