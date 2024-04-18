using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLOneBot.Net.ComWebHelper
{
    //
    // 摘要:
    //     Specifies the media type information for an email message attachment.
    /// <summary>
    /// 
    /// </summary>
    public static class CustomMediaTypeNames
    {
        //
        // 摘要:
        //     Specifies the kind of application data in an email message attachment.
        /// <summary>
        /// 
        /// </summary>
        public static class Application
        {
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in URL
            //     encoded format.
            /// <summary>
            /// 
            /// </summary>
            public const string FormUrlEncoded = "application/x-www-form-urlencoded";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in JSON
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string Json = "application/json";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in JSON
            //     patch format.
            /// <summary>
            /// 
            /// </summary>
            public const string JsonPatch = "application/json-patch+json";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in JSON
            //     text sequence format.
            /// <summary>
            /// 
            /// </summary>
            public const string JsonSequence = "application/json-seq";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Web
            //     Application Manifest.
            /// <summary>
            /// 
            /// </summary>
            public const string Manifest = "application/manifest+json";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is not interpreted.
            /// <summary>
            /// 
            /// </summary>
            public const string Octet = "application/octet-stream";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Portable
            //     Document Format (PDF).
            /// <summary>
            /// 
            /// </summary>
            public const string Pdf = "application/pdf";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in JSON
            //     problem detail format.
            /// <summary>
            /// 
            /// </summary>
            public const string ProblemJson = "application/problem+json";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in XML
            //     problem detail format.
            /// <summary>
            /// 
            /// </summary>
            public const string ProblemXml = "application/problem+xml";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Rich
            //     Text Format (RTF).
            /// <summary>
            /// 
            /// </summary>
            public const string Rtf = "application/rtf";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is a SOAP
            //     document.
            /// <summary>
            /// 
            /// </summary>
            public const string Soap = "application/soap+xml";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in WASM
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string Wasm = "application/wasm";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in XML
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string Xml = "application/xml";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in XML
            //     Document Type Definition format.
            /// <summary>
            /// 
            /// </summary>
            public const string XmlDtd = "application/xml-dtd";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is in XML
            //     patch format.
            /// <summary>
            /// 
            /// </summary>
            public const string XmlPatch = "application/xml-patch+xml";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Application data is compressed.
            /// <summary>
            /// 
            /// </summary>
            public const string Zip = "application/zip";
        }
        //
        // 摘要:
        //     Specifies the kind of font data in an email message attachment.
        /// <summary>
        /// 
        /// </summary>
        public static class Font
        {
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in font type collection
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string Collection = "font/collection";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in OpenType Layout
            //     (OTF) format.
            /// <summary>
            /// 
            /// </summary>
            public const string Otf = "font/otf";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in SFNT format.
            /// <summary>
            /// 
            /// </summary>
            public const string Sfnt = "font/sfnt";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in TrueType font
            //     (TTF) format.
            /// <summary>
            /// 
            /// </summary>
            public const string Ttf = "font/ttf";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in WOFF format.
            /// <summary>
            /// 
            /// </summary>
            public const string Woff = "font/woff";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Font data is in WOFF2 format.
            /// <summary>
            /// 
            /// </summary>
            public const string Woff2 = "font/woff2";
        }
        //
        // 摘要:
        //     Specifies the type of image data in an email message attachment.
        /// <summary>
        /// 
        /// </summary>
        public static class Image
        {
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in AVIF format.
            /// <summary>
            /// 
            /// </summary>
            public const string Avif = "image/avif";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in BMP format.
            /// <summary>
            /// 
            /// </summary>
            public const string Bmp = "image/bmp";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Graphics Interchange
            /// <summary>
            /// 
            /// </summary>
            //     Format (GIF).
            public const string Gif = "image/gif";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in ICO format.
            /// <summary>
            /// 
            /// </summary>
            public const string Icon = "image/x-icon";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Joint Photographic
            /// <summary>
            /// 
            /// </summary>
            //     Experts Group (JPEG) format.
            public const string Jpeg = "image/jpeg";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in PNG format.
            /// <summary>
            /// 
            /// </summary>
            public const string Png = "image/png";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in SVG format.
            /// <summary>
            /// 
            /// </summary>
            public const string Svg = "image/svg+xml";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in Tagged Image
            /// <summary>
            /// 
            /// </summary>
            //     File Format (TIFF).
            public const string Tiff = "image/tiff";
            /// <summary>
            /// 
            /// </summary>
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Image data is in WEBP format.
            public const string Webp = "image/webp";
           
        }
        //
        // 摘要:
        //     Specifies the kind of multipart data in an email message attachment.
        /// <summary>
        /// 
        /// </summary>
        public static class Multipart
        {
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Multipart data consists of
            //     multiple byte ranges.
            /// <summary>
            /// 
            /// </summary>
            public const string ByteRanges = "multipart/byteranges";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Multipart data is in format.
            /// <summary>
            /// 
            /// </summary>
            public const string FormData = "multipart/form-data";
        }
        //
        // 摘要:
        //     Specifies the type of text data in an email message attachment.
        /// <summary>
        /// 
        /// </summary>
        public static class Text
        {
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in CSS format.
            /// <summary>
            /// 
            /// </summary>
            public const string Css = "text/css";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in CSV format.
            /// <summary>
            /// 
            /// </summary>
            public const string Csv = "text/csv";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in HTML format.
            /// <summary>
            /// 
            /// </summary>
            public const string Html = "text/html";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Javascript
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string JavaScript = "text/javascript";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Markdown format.
            /// <summary>
            /// 
            /// </summary>
            public const string Markdown = "text/markdown";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in plain text
            //     format.
            /// <summary>
            /// 
            /// </summary>
            public const string Plain = "text/plain";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Rich Text Format
            //     (RTF).
            /// <summary>
            /// 
            /// </summary>
            public const string RichText = "text/richtext";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in Rich Text Format
            //     (RTF).
            /// <summary>
            /// 
            /// </summary>
            public const string Rtf = "text/rtf";
            //
            // 摘要:
            //     Specifies that the System.Net.Mime.MediaTypeNames.Text data is in XML format.
            /// <summary>
            /// 
            /// </summary>
            public const string Xml = "text/xml";
        }
    }
}
