using System;
using System.IO;
using System.Text;
using System.Web;

namespace WebCache.Static
{
	public class StaticContentFilter : Stream
	{
		private static readonly char[] HrefAttribute;

		private static readonly char[] RelAttribute;

		private static readonly char[] HttpPrefix;

		private static readonly char[] ImgTag;

		private static readonly char[] LinkTag;

		private static readonly char[] ScriptTag;

		private static readonly char[] SrcAttribute;

		private byte[] _cssPrefix;

		private Encoding _encoding;

		private byte[] _imagePrefix;

		private byte[] _javascriptPrefix;

		private char[] _applicationPath;

		private byte[] _baseUrl;

		private byte[] _currentFolder;

		private char[] _pendingBuffer;

		private Stream _responseStream;

		private Func<string, string> _getVersionOfFile;

		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		public override long Length
		{
			get
			{
				return 0;
			}
		}

		public override long Position
		{
			get;
			set;
		}

		static StaticContentFilter()
		{
			HrefAttribute = "href".ToCharArray();
			RelAttribute = "rel".ToCharArray();
			HttpPrefix = "http://".ToCharArray();
			ImgTag = "img".ToCharArray();
			LinkTag = "link".ToCharArray();
			ScriptTag = "script".ToCharArray();
			SrcAttribute = "src".ToCharArray();
		}

		public StaticContentFilter(HttpResponse response, Func<string, string> getVersionOfFile, string imagePrefix, string javascriptPrefix, string cssPrefix, string baseUrl, string applicationPath, string currentRelativePath)
		{
			_encoding = response.Output.Encoding;
			_responseStream = response.Filter;
			_imagePrefix = _encoding.GetBytes(imagePrefix);
			_javascriptPrefix = _encoding.GetBytes(javascriptPrefix);
			_cssPrefix = _encoding.GetBytes(cssPrefix);
			_applicationPath = applicationPath.ToCharArray();
			_baseUrl = _encoding.GetBytes(baseUrl);
			_currentFolder = _encoding.GetBytes(currentRelativePath);
			_getVersionOfFile = getVersionOfFile;
		}

		public override void Close()
		{
			FlushPendingBuffer();
			_responseStream.Close();
			_responseStream = null;
			_getVersionOfFile = null;
			_pendingBuffer = null;
		}

		private int FindAttributeValuePos(char[] attributeName, char[] content, int pos)
		{
			for (int i = pos; i < content.Length - attributeName.Length; i++)
			{
				if (62 == content[i])
				{
					return -1;
				}
				if (HasMatch(content, i, attributeName))
				{
					pos = i + attributeName.Length;
					int num = pos;
					pos = num + 1;
					if (34 != content[num])
					{
						return pos;
					}
				}
			}
			return -1;
		}

		public override void Flush()
		{
			FlushPendingBuffer();
			_responseStream.Flush();
		}

		private void FlushPendingBuffer()
		{
			if (_pendingBuffer != null)
			{
				WriteOutput(_pendingBuffer, 0, _pendingBuffer.Length);
				_pendingBuffer = null;
			}
		}

		private bool HasMatch(char[] content, int pos, char[] match)
		{
			for (int i = 0; i < match.Length; i++)
			{
				if (content[pos + i] != match[i] && content[pos + i] != match[i])
				{
					return false;
				}
			}
			return true;
		}

		private bool HasTagEnd(char[] content, int pos)
		{
			while (pos < content.Length)
			{
				if (62 == content[pos])
				{
					return true;
				}
				pos++;
			}
			return false;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return _responseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _responseStream.Seek(offset, origin);
		}

		public override void SetLength(long length)
		{
			_responseStream.SetLength(length);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			char[] chrArray;
			char[] chars = _encoding.GetChars(buffer, offset, count);
			if (_pendingBuffer == null)
			{
				chrArray = chars;
			}
			else
			{
				chrArray = new char[chars.Length + _pendingBuffer.Length];
				Array.Copy(_pendingBuffer, 0, chrArray, 0, _pendingBuffer.Length);
				Array.Copy(chars, 0, chrArray, _pendingBuffer.Length, chars.Length);
				_pendingBuffer = null;
			}
			int num = 0;
			for (int i = 0; i < chrArray.Length; i++)
			{
				if (60 == chrArray[i])
				{
					i++;
					if (!HasTagEnd(chrArray, i))
					{
						_pendingBuffer = new char[chrArray.Length - i];
						Array.Copy(chrArray, i, _pendingBuffer, 0, chrArray.Length - i);
						WriteOutput(chrArray, num, i - num);
						return;
					}
					if (47 != chrArray[i])
					{
						if (HasMatch(chrArray, i, ImgTag))
						{
							num = WritePrefixIf(SrcAttribute, chrArray, i, num, _imagePrefix);
						}
						else if (HasMatch(chrArray, i, ScriptTag))
						{
							num = WritePrefixIf(SrcAttribute, chrArray, i, num, _javascriptPrefix);
							num = WritePathWithVersion(chrArray, num);
						}
						else if (HasMatch(chrArray, i, LinkTag))
						{
							num = WritePrefixIf(HrefAttribute, chrArray, i, num, _cssPrefix);
							num = WritePathWithVersion(chrArray, num);
						}
						if (num > i)
						{
							i = num;
						}
					}
				}
			}
			WriteOutput(chrArray, num, chrArray.Length - num);
		}

		private void WriteBytes(byte[] bytes, int pos, int length)
		{
			_responseStream.Write(bytes, 0, bytes.Length);
		}

		private void WriteOutput(char[] content, int pos, int length)
		{
			if (length == 0)
			{
				return;
			}
			byte[] bytes = _encoding.GetBytes(content, pos, length);
			WriteBytes(bytes, 0, bytes.Length);
		}

		private int WritePathWithVersion(char[] content, int lastPosWritten)
		{
			if (!HasMatch(content, lastPosWritten, HttpPrefix))
			{
				int num = lastPosWritten + 1;
				while (34 != content[num])
				{
					num++;
				}
				string str = new string(content, lastPosWritten, num - lastPosWritten);
				WriteOutput(content, lastPosWritten, num - lastPosWritten);
				lastPosWritten = num;
				char[] charArray = _getVersionOfFile(str).ToCharArray();
				WriteOutput(charArray, 0, charArray.Length);
			}
			return lastPosWritten;
		}

		private int WritePrefixIf(char[] attributeName, char[] content, int pos, int lastWritePos, byte[] prefix)
		{
			int length = FindAttributeValuePos(attributeName, content, pos);
			if (length <= 0)
			{
				return lastWritePos;
			}
			if (HasMatch(content, length, HttpPrefix))
			{
				return lastWritePos;
			}
			WriteOutput(content, lastWritePos, length - lastWritePos);
			if (prefix.Length != 0)
			{
				WriteBytes(prefix, 0, prefix.Length);
			}
			if (HasMatch(content, length, _applicationPath))
			{
				length = length + _applicationPath.Length;
			}
			else if (_currentFolder.Length != 0)
			{
				WriteBytes(_currentFolder, 0, _currentFolder.Length);
			}
			if (47 == content[length])
			{
				length++;
			}
			return length;
		}
	}
}