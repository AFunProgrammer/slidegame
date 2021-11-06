using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Slide
{
    class easyXMLReader : System.Object
    {
        private StreamReader m_xmlStream;
        public easyXMLReader(StreamReader xmlStreamReader)
        {
            if (xmlStreamReader == null)
            {
                throw new NullReferenceException();
            }

            if (xmlStreamReader.BaseStream.CanSeek == false)
            {
                throw new Exception("StreamReader needs to be able to seek try opening file for read exclusive");
            }

            m_xmlStream = xmlStreamReader;
        }

        public string ReadToNextNode()
        {
            string strNodeName = "";

            //consume till beginning of node name e.g. '<' found
            while (m_xmlStream.EndOfStream == false)
            {
                if ((char)m_xmlStream.Peek() == '<')
                {
                    m_xmlStream.Read();

                    if (Char.IsLetterOrDigit((char)m_xmlStream.Peek()))
                    {
                        break;
                    }
                }

                m_xmlStream.Read();
            }

            while ((m_xmlStream.EndOfStream == false))
            {
                strNodeName += (char)m_xmlStream.Read();
                if (strNodeName.Last() == '>')
                {
                    break;
                }
            }

            //remove start and trailing symbols
            strNodeName = strNodeName.Trim('<', '>');

            return strNodeName;
        }

        public string ReadNodeValue()
        {
            string strValue = "";

            while (m_xmlStream.EndOfStream == false && (char)m_xmlStream.Peek() != '<')
            {
                strValue += (char)m_xmlStream.Read();
            }

            return strValue.Trim();
        }
        public bool HasValue()
        {
            string strValue = "";
            long lPos = m_xmlStream.BaseStream.Position;

            strValue = ReadNodeValue();

            m_xmlStream.BaseStream.Position = lPos;

            if (strValue.Length > 0)
                return true;

            return false;
        }

        public void Close()
        {
            m_xmlStream.Close();
            m_xmlStream.Dispose();
        }
    }//End of easyXmlReader
}
