/*
* Creative Commons Attribution-Share Alike 3.0 Unported
* You are free:
*   to Share — to copy, distribute and transmit the work
*   to Remix — to adapt the work
*
* Under the following conditions:
*   Attribution — You must attribute the work in the manner
*   specified by the author or licensor (but not in any way
*   that suggests that they endorse you or your use of the work).
*
*   Share Alike — If you alter, transform, or build upon this work,
*   you may distribute the resulting work only under the same, similar
*   or a compatible license.
*
* For more information, see http://creativecommons.org/licenses/by-sa/3.0/
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

/*
 * This class is completely based on the documentation
 * of the EXIF format found here: http://www.exif.org/Exif2-2.PDF
 */

namespace FluentImageResizing
{
    public static class ImageExtensions
    {
        #region Enumerations
#pragma warning disable 1591

        private enum PropertyType
        {
            ByteArray = 1,
            AsciiString = 2,
            UnsignedShort = 3,
            UnsignedLong = 4,
            UnsignedLongPair = 5,
            Any = 6,
            SignedLongArray = 7,
            UnsignedLongPairArray = 10
        }

        public enum PropertyTag
        {
            ImageWidth = 0x100,
            ImageLength = 0x101,
            NBitsPerSample = 0x102,
            Compression = 0x103,
            PhotometricInterpretation = 0x106,
            Orientation = 0x112,
            SamplesPerPixel = 0x115,
            PlanarConfiguration = 0x11C,
            YCbCrSubSampling = 0x212,
            YCbCrPositioning = 0x213,
            XResolution = 0x11A,
            YResolution = 0x11B,
            ResolutionUnit = 0x128,
            StripOffsets = 0x111,
            RowsPerStrip = 0x116,
            StripByteCounts = 0x117,
            JPEGInterchangeFormat = 0x201,
            JPEGInterchangeFormatLength = 0x202,
            TransferFunction = 0x12D,
            WhitePoint = 0x13E,
            PrimaryChromaticities = 0x13F,
            YCbCrCoefficients = 0x211,
            ReferenceBlackWhite = 0x214,
            DateTime = 0x132,
            ImageDescription = 0x10E,
            Make = 0x10F,
            Model = 0x110,
            Software = 0x131,
            Artist = 0x13B,
            Copyright = 0x8298,
            ExifVersion = 0x9000,
            FlashpixVersion = 0xA000,
            ColorSpace = 0xA001,
            ComponentsConfiguration = 0x9101,
            CompressedBitsPerPixel = 0x9102,
            PixelXDimension = 0xA002,
            PixelYDimension = 0xA003,
            MakerNote = 0x927C,
            UserComment = 0x9286,
            RelatedSoundFile = 0xA004,
            DateTimeOriginal = 0x9003,
            DateTimeDigitized = 0x9004,
            SubSecTime = 0x9290,
            SubSecTimeOriginal = 0x9291,
            SubSecTimeDigitized = 0x9292,
            ImageUniqueID = 0xA420,
            ExposureTime = 0x829A,
            FNumber = 0x829D,
            ExposureProgram = 0x8822,
            SpectralSensitivity = 0x8824,
            ISOSpeedRatings = 0x8827,
            OECF = 0x8828,
            ShutterSpeedValue = 0x9201,
            ApertureValue = 0x9202,
            BrightnessValue = 0x9203,
            ExposureBiasValue = 0x9204,
            MaxApertureValue = 0x9205,
            SubjectDistance = 0x9206,
            MeteringMode = 0x9207,
            LightSource = 0x9208,
            Flash = 0x9209,
            FocalLength = 0x920A,
            SubjectArea = 0x9214,
            FlashEnergy = 0xA20B,
            SpatialFrequencyResponse = 0xA20C,
            FocalPlaneXResolution = 0xA20E,
            FocalPlaneYResolution = 0xA20F,
            FocalPlaneResolutionUnit = 0xA210,
            SubjectLocation = 0xA214,
            ExposureIndex = 0xA215,
            SensingMethod = 0xA217,
            FileSource = 0xA300,
            SceneType = 0xA301,
            CFAPattern = 0xA302,
            CustomRendered = 0xA401,
            ExposureMode = 0xA402,
            WhiteBalance = 0xA403,
            DigitalZoomRatio = 0xA404,
            FocalLengthIn35mmFilm = 0xA405,
            SceneCaptureType = 0xA406,
            GainControl = 0xA407,
            Contrast = 0xA408,
            Saturation = 0xA409,
            Sharpness = 0xA40A,
            DeviceSettingDescription = 0xA40B,
            SubjectDistanceRange = 0xA40C
        }

        public enum Compression
        {
            NotSpecified = 0,
            Uncompressed = 1,
            JpegCompression = 6
        }

        public enum PhotometricInterpretation
        {
            RGB = 2,
            YCbCr = 6
        }

        public enum Orientation
        {
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LeftBottom = 8
        }

        public enum ExposureMode
        {
            AutoExposure = 0,
            ManualExposure = 1,
            AutoBracket = 2
        }

        public enum ExposureProgram
        {
            Undefined = 0,
            Manual = 1,
            Normal = 2,
            AperturePriority = 3,
            ShutterPriority = 4,
            CreativeProgram = 5,
            ActionProgram = 6,
            PortraitMode = 7,
            LandscapeMode = 8
        }

        public enum MeteringMode
        {
            Unknown = 0,
            Average = 1,
            CenterWeightedAverage = 2,
            Spot = 3,
            MultiSpot = 4,
            Pattern = 5,
            Partial = 6,
            Other = 255
        }

        public enum Flash
        {
            FlashDidNotFire = 0x0000,
            FlashFired = 0x0001,
            StrobeReturnNotDetected = 0x0005,
            StrobeReturnDetected = 0x0007,
            FlashFiredCompulsoryMode = 0x0009,
            FlashFiredCompulsoryModeStrobeReturnNotDetected = 0x000D,
            FlashFiredCompulsoryModeStrobeReturnDetected = 0x000F,
            FlashDidNotFireCompulsoryMode = 0x0010,
            FlashDidNotFireAutoMode = 0x0018,
            FlashFiredAutoMode = 0x00019,
            FlashFiredAutoModeStrobeReturnNotDetected = 0x001D,
            FlashFiredAutoModeStrobeReturnDetected = 0x001F,
            NoFlash = 0x0020,
            FlashFiredRedEyeReduction = 0x0041,
            FlashFiredRedEyeReductionStrobeReturnNotDetected = 0x0045,
            FlashFiredRedEyeReductionStrobeReturnDetected = 0x0047,
            FlashFiredCompulsoryModeRedEyeReduction = 0x0049,
            FlashFiredCompulsoryModeRedEyeReductionStrobeReturnNotDetected = 0x004D,
            FlashFiredCompulsoryModeRedEyeReductionStrobeReturnDetected = 0x004F,
            FlashFiredAutoModeRedEyeReduction = 0x0059,
            FlashFiredAutoModeRedEyeReductionStrobeReturnNotDetected = 0x005D,
            FlashFiredAutoModeRedEyeReductionStrobeReturnDetected = 0x005F
        }

        public enum WhiteBalance
        {
            Auto = 0,
            Manual = 1
        }

        public enum SceneCaptureType
        {
            Standard = 0,
            Landscape = 1,
            Portrait = 2,
            NightScene = 3
        }

        public enum LightSource
        {
            Unkown = 0,
            Daylight = 1,
            Fluorescent = 2,
            Tungsten = 3,
            Flash = 4,
            FineWeather = 9,
            CloudyWeather = 10,
            Shade = 11,
            DaylightFluorescent = 12,
            DayWhiteFluorescent = 13,
            CoolWhiteFluorescent = 14,
            WhiteFluorescent = 15,
            StandardLightA = 17,
            StandardLightB = 18,
            StandardLightC = 19,
            D55 = 20,
            D65 = 21,
            D75 = 22,
            D50 = 23,
            ISOStudioTungsten = 24,
            OtherLightSource = 255
        }

        public enum GainControl
        {
            None = 0,
            LowGainUp = 1,
            HighGainUp = 2,
            LowGainDown = 3,
            HighGainDown = 4
        }

        public enum Contrast
        {
            Normal = 0,
            Soft = 1,
            Hard = 2
        }

        public enum Saturation
        {
            Normal = 0,
            LowSaturation = 1,
            HighSaturation = 2
        }

        public enum Sharpness
        {
            Normal = 0,
            Soft = 1,
            Hard = 2
        }

        public enum SubjectDistanceRange
        {
            Unknown = 0,
            Macro = 1,
            CloseView = 2,
            DistantView = 3
        }

#pragma warning restore 1591
        #endregion

        #region Properties

        public static DateTime ExifDateTime(this Image image)
        {
            return GetPropertyDateTime(image, PropertyTag.DateTime);
        }

        public static DateTime ExifDateTimeDigitized(this Image image)
        {
            return GetPropertyDateTime(image, PropertyTag.DateTimeDigitized);
        }

        public static DateTime ExifDateTimeOriginal(this Image image)
        {
            return GetPropertyDateTime(image, PropertyTag.DateTimeOriginal);
        }

        public static string ExifMake(this Image image)
        {
            return GetPropertyString(image, PropertyTag.Make);
        }

        public static string ExifModel(this Image image)
        {
            return GetPropertyString(image, PropertyTag.Model);
        }

        public static string ExifImageDescription(this Image image)
        {
            return GetPropertyString(image, PropertyTag.ImageDescription);
        }

        public static string ExifSoftware(this Image image)
        {
            return GetPropertyString(image, PropertyTag.Software);
        }

        public static string ExifArtist(this Image image)
        {
            return GetPropertyString(image, PropertyTag.Artist);
        }

        public static Orientation ExifOrientation(this Image image)
        {
            return (Orientation)GetPropertyUInt16(image, PropertyTag.Orientation);
        }

        public static Flash ExifFlash(this Image image)
        {
            return (Flash)GetPropertyUInt16(image, PropertyTag.Flash);
        }

        public static SceneCaptureType ExifSceneCaptureType(this Image image)
        {
            return (SceneCaptureType)GetPropertyUInt16(image, PropertyTag.SceneCaptureType);
        }

        public static WhiteBalance ExifWhiteBalance(this Image image)
        {
            return (WhiteBalance)GetPropertyUInt16(image, PropertyTag.WhiteBalance);
        }

        public static MeteringMode ExifMeteringMode(this Image image)
        {
            return (MeteringMode)GetPropertyUInt16(image, PropertyTag.MeteringMode);
        }

        public static Compression ExifCompression(this Image image)
        {
            return (Compression)GetPropertyUInt16(image, PropertyTag.Compression);
        }

        public static ExposureMode ExifExposureMode(this Image image)
        {
            return (ExposureMode)GetPropertyUInt16(image, PropertyTag.ExposureMode);
        }

        public static ExposureProgram ExifExposureProgram(this Image image)
        {
            return (ExposureProgram)GetPropertyUInt16(image, PropertyTag.ExposureProgram);
        }

        public static PhotometricInterpretation ExifPhotometricInterpretation(this Image image)
        {
            return (PhotometricInterpretation)GetPropertyUInt16(image, PropertyTag.PhotometricInterpretation);
        }

        public static LightSource ExifLightSource(this Image image)
        {
            return (LightSource)GetPropertyUInt16(image, PropertyTag.LightSource);
        }

        public static GainControl ExifGainControl(this Image image)
        {
            return (GainControl)GetPropertyUInt16(image, PropertyTag.GainControl);
        }

        public static Contrast ExifLightContrast(this Image image)
        {
            return (Contrast)GetPropertyUInt16(image, PropertyTag.Contrast);
        }

        public static Saturation ExifSaturation(this Image image)
        {
            return (Saturation)GetPropertyUInt16(image, PropertyTag.Saturation);
        }

        public static Sharpness ExifSharpness(this Image image)
        {
            return (Sharpness)GetPropertyUInt16(image, PropertyTag.Sharpness);
        }

        public static SubjectDistanceRange ExifSubjectDistanceRange(this Image image)
        {
            return (SubjectDistanceRange)GetPropertyUInt16(image, PropertyTag.SubjectDistanceRange);
        }

        public static Rational ExifExposureTime(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.ExposureTime);
        }

        public static double ExifFNumber(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.FNumber).ToDouble();
        }

        public static double ExifAperture(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.ApertureValue).ToDouble();
        }

        public static Rational ExifSubjectDistance(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.SubjectDistance);
        }

        public static double ExifFocalLength(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.FocalLength).ToDouble();
        }

        public static UInt16 ExifISOSpeedRating(this Image image)
        {
            return GetPropertyUInt16(image, PropertyTag.ISOSpeedRatings);
        }

        public static Rational ExifDigitalZoomRatio(this Image image)
        {
            return GetPropertyRational(image, PropertyTag.DigitalZoomRatio);
        }

        #endregion

        public static bool HasProperty(this Image image, PropertyTag property)
        {
            return Array.IndexOf(image.PropertyIdList, (Int32)property) > -1;
        }

        public static DateTime GetPropertyDateTime(this Image image, PropertyTag property)
        {
            if (!image.HasProperty(property))
                return DateTime.MinValue;

            string value = GetPropertyString(image, property);

            // If first char is blank or string is empty no DateTime is present
            if (value == string.Empty || value[0] == ' ')
                return DateTime.MinValue;

            // DateTime is written as YYYY:MM:DD HH:MM:SS
            // and needs to be converted to YYYY-MM-DD HH:MM:SS
            // to be convertable to a .Net DateTime
            string datePart = value.Substring(0, 10).Replace(':', '-');
            string timePart = value.Substring(11);
            return Convert.ToDateTime(datePart + ' ' + timePart);
        }

        public static string GetPropertyString(this Image image, PropertyTag property)
        {
            if (!image.HasProperty(property))
                return string.Empty;

            PropertyItem item = image.GetPropertyItem((Int32)property);
            if (item.Type != (short)PropertyType.AsciiString)
                return string.Empty;

            byte[] buffer = item.Value;
            return Encoding.ASCII.GetString(buffer).Trim('\0');
        }

        public static UInt32 GetPropertyUInt32(this Image image, PropertyTag property)
        {
            if (!image.HasProperty(property))
                return 0;

            PropertyItem item = image.GetPropertyItem((Int32)property);
            if (item.Type != (short)PropertyType.UnsignedLong)
                return 0;

            byte[] buffer = item.Value;
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static UInt16 GetPropertyUInt16(this Image image, PropertyTag property)
        {
            if (!image.HasProperty(property))
                return 0;

            PropertyItem item = image.GetPropertyItem((Int32)property);
            if (item.Type != (short)PropertyType.UnsignedShort)
                return 0;

            byte[] buffer = item.Value;
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static Rational GetPropertyRational(this Image image, PropertyTag property)
        {
            if (!image.HasProperty(property))
                return Rational.Zero;

            PropertyItem item = image.GetPropertyItem((Int32)property);
            if (item.Type != (short)PropertyType.UnsignedLongPair)
                return Rational.Zero;

            byte[] buffer = item.Value;
            Int32 numerator = BitConverter.ToInt32(buffer, 0);
            Int32 denominator = BitConverter.ToInt32(buffer, 4);

            Rational rational = new Rational(numerator, denominator);
            return rational;
        }
    }

    public struct Rational
    {
        private Int32 _numerator;
        public Int32 Numerator
        {
            get
            {
                return _numerator;
            }
            set
            {
                _numerator = value;
            }
        }

        private Int32 _denominator;
        public Int32 Denominator
        {
            get
            {
                return _denominator;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("Denominator", 0, "Denominator can't be zero.");

                _denominator = value;
            }
        }

        public static Rational Zero
        {
            get
            {
                return new Rational(0, 1);
            }
        }

        public Rational(Int32 numerator, Int32 denominator)
        {
            _numerator = 0;
            _denominator = 1;
            Numerator = numerator;
            Denominator = denominator;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }

        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }
    }
}