using System;
using System.Text;

namespace Mod.DungPham.KoiOctiiu957
{
	public class VietnameseTelex
	{
		public static bool isEnabled = true;

		private static readonly char[][] VOWEL_TABLE = new char[][]
		{
			// a (0..2)
			new char[] { 'a', 'á', 'à', 'ả', 'ã', 'ạ' },
			new char[] { 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ' },
			new char[] { 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ' },
			// e (3..4)
			new char[] { 'e', 'é', 'è', 'ẻ', 'ẽ', 'ẹ' },
			new char[] { 'ê', 'ế', 'ề', 'ể', 'ễ', 'ệ' },
			// i (5)
			new char[] { 'i', 'í', 'ì', 'ỉ', 'ĩ', 'ị' },
			// o (6..8)
			new char[] { 'o', 'ó', 'ò', 'ỏ', 'õ', 'ọ' },
			new char[] { 'ô', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ' },
			new char[] { 'ơ', 'ớ', 'ờ', 'ở', 'ỡ', 'ợ' },
			// u (9..10)
			new char[] { 'u', 'ú', 'ù', 'ủ', 'ũ', 'ụ' },
			new char[] { 'ư', 'ứ', 'ừ', 'ử', 'ữ', 'ự' },
			// y (11)
			new char[] { 'y', 'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ' },

			// UPPERCASE
			// A (12..14)
			new char[] { 'A', 'Á', 'À', 'Ả', 'Ã', 'Ạ' },
			new char[] { 'Ă', 'Ắ', 'Ằ', 'Ẳ', 'Ẵ', 'Ặ' },
			new char[] { 'Â', 'Ấ', 'Ầ', 'Ẩ', 'Ẫ', 'Ậ' },
			// E (15..16)
			new char[] { 'E', 'É', 'È', 'Ẻ', 'Ẽ', 'Ẹ' },
			new char[] { 'Ê', 'Ế', 'Ề', 'Ể', 'Ễ', 'Ệ' },
			// I (17)
			new char[] { 'I', 'Í', 'Ì', 'Ỉ', 'Ĩ', 'Ị' },
			// O (18..20)
			new char[] { 'O', 'Ó', 'Ò', 'Ỏ', 'Õ', 'Ọ' },
			new char[] { 'Ô', 'Ố', 'Ồ', 'Ổ', 'Ỗ', 'Ộ' },
			new char[] { 'Ơ', 'Ớ', 'Ờ', 'Ở', 'Ỡ', 'Ợ' },
			// U (21..22)
			new char[] { 'U', 'Ú', 'Ù', 'Ủ', 'Ũ', 'Ụ' },
			new char[] { 'Ư', 'Ứ', 'Ừ', 'Ử', 'Ữ', 'Ự' },
			// Y (23)
			new char[] { 'Y', 'Ý', 'Ỳ', 'Ỷ', 'Ỹ', 'Ỵ' }
		};

		private static bool IsDelimiter(char c)
		{
			return char.IsWhiteSpace(c) || c == '.' || c == ',' || c == '/' || c == ':' || c == ';' ||
				   c == '!' || c == '?' || c == '-' || c == '+' || c == '=' || c == '(' || c == ')' ||
				   c == '[' || c == ']' || c == '{' || c == '}' || c == '@' || c == '#' || c == '$' ||
				   c == '%' || c == '^' || c == '&' || c == '*' || c == '<' || c == '>' || c == '"' ||
				   c == '\'' || c == '\\' || c == '|' || c == '`' || c == '~';
		}

		private static int GetCharRow(char c, out int tone)
		{
			for (int r = 0; r < VOWEL_TABLE.Length; r++)
			{
				for (int t = 0; t < 6; t++)
				{
					if (VOWEL_TABLE[r][t] == c)
					{
						tone = t;
						return r;
					}
				}
			}
			tone = 0;
			return -1;
		}

		private static bool IsVowel(char c)
		{
			int tone;
			return GetCharRow(c, out tone) != -1;
		}

		private static char ChangeVowelTone(char c, int newTone)
		{
			int tone;
			int row = GetCharRow(c, out tone);
			if (row != -1 && newTone >= 0 && newTone <= 5)
			{
				return VOWEL_TABLE[row][newTone];
			}
			return c;
		}

		private static char GetBaseVowel(char c)
		{
			return ChangeVowelTone(c, 0);
		}

		private static int GetCurrentWordTone(string word)
		{
			for (int i = 0; i < word.Length; i++)
			{
				int tone;
				int row = GetCharRow(word[i], out tone);
				if (row != -1 && tone > 0)
				{
					return tone;
				}
			}
			return 0;
		}

		private static string RemoveWordTone(string word)
		{
			StringBuilder sb = new StringBuilder(word.Length);
			for (int i = 0; i < word.Length; i++)
			{
				sb.Append(GetBaseVowel(word[i]));
			}
			return sb.ToString();
		}

		private static int FindTonePosition(string word)
		{
			int[] vowelIdx = new int[word.Length];
			int vowelCount = 0;
			for (int i = 0; i < word.Length; i++)
			{
				if (IsVowel(word[i]))
				{
					vowelIdx[vowelCount++] = i;
				}
			}

			if (vowelCount == 0) return -1;
			if (vowelCount == 1) return vowelIdx[0];

			int startVowel = 0;
			if (word.Length >= 2)
			{
				string lower = word.ToLower();
				if (lower.StartsWith("qu") && vowelCount > 1 && vowelIdx[0] == 1)
				{
					startVowel = 1;
				}
				else if (lower.StartsWith("gi") && vowelCount > 1 && vowelIdx[0] == 1)
				{
					startVowel = 1;
				}
			}

			int actualVowelCount = vowelCount - startVowel;
			if (actualVowelCount == 1)
			{
				return vowelIdx[startVowel];
			}

			bool hasEndingConsonant = !IsVowel(word[word.Length - 1]);

			if (actualVowelCount == 2)
			{
				if (hasEndingConsonant)
				{
					return vowelIdx[startVowel + 1];
				}
				else
				{
					char v1 = char.ToLower(GetBaseVowel(word[vowelIdx[startVowel]]));
					char v2 = char.ToLower(GetBaseVowel(word[vowelIdx[startVowel + 1]]));

					if ((v1 == 'o' && (v2 == 'a' || v2 == 'e')) || (v1 == 'u' && v2 == 'y'))
					{
						return vowelIdx[startVowel + 1];
					}
					if (v2 == 'a' && (v1 == 'i' || v1 == 'u' || v1 == 'ư'))
					{
						return vowelIdx[startVowel];
					}
					return vowelIdx[startVowel];
				}
			}

			if (actualVowelCount >= 3)
			{
				return vowelIdx[startVowel + 1];
			}

			return vowelIdx[startVowel];
		}

		private static string ApplyTone(string word, int targetTone)
		{
			string cleanWord = RemoveWordTone(word);
			if (targetTone == 0)
			{
				return cleanWord;
			}

			int pos = FindTonePosition(cleanWord);
			if (pos == -1)
			{
				return word;
			}

			StringBuilder sb = new StringBuilder(cleanWord);
			sb[pos] = ChangeVowelTone(cleanWord[pos], targetTone);
			return sb.ToString();
		}

		public static bool ProcessKey(string strBefore, char key, out string result)
		{
			result = strBefore;
			if (!isEnabled) return false;

			if (strBefore == null || strBefore.Length == 0)
			{
				return false;
			}

			int wordStart = strBefore.Length;
			while (wordStart > 0 && !IsDelimiter(strBefore[wordStart - 1]))
			{
				wordStart--;
			}

			string prefix = strBefore.Substring(0, wordStart);
			string word = strBefore.Substring(wordStart);

			if (word.Length == 0)
			{
				return false;
			}

			char lowerKey = char.ToLower(key);

			// 1. Tone keys: s, f, r, x, j, z
			int targetTone = -1;
			switch (lowerKey)
			{
				case 's': targetTone = 1; break;
				case 'f': targetTone = 2; break;
				case 'r': targetTone = 3; break;
				case 'x': targetTone = 4; break;
				case 'j': targetTone = 5; break;
				case 'z': targetTone = 0; break;
			}

			if (targetTone != -1)
			{
				int currentTone = GetCurrentWordTone(word);
				int tonePos = FindTonePosition(word);

				if (tonePos != -1)
				{
					if (targetTone != 0 && currentTone == targetTone)
					{
						result = prefix + RemoveWordTone(word) + key;
						return true;
					}

					string tonedWord = ApplyTone(word, targetTone);
					if (tonedWord != word || targetTone == 0)
					{
						result = prefix + tonedWord;
						return true;
					}
				}
			}

			// 2. Vowel modifiers
			if (lowerKey == 'd')
			{
				for (int i = word.Length - 1; i >= 0; i--)
				{
					if (word[i] == 'd')
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = 'đ';
						result = prefix + sb.ToString();
						return true;
					}
					if (word[i] == 'D')
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = 'Đ';
						result = prefix + sb.ToString();
						return true;
					}
					if (word[i] == 'đ')
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = 'd';
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (word[i] == 'Đ')
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = 'D';
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
				}
			}

			if (lowerKey == 'a')
			{
				for (int i = word.Length - 1; i >= 0; i--)
				{
					int tone;
					int row = GetCharRow(word[i], out tone);
					if (row == 0 || row == 1)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[2][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 12 || row == 13)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[14][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 2)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[0][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 14)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[12][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
				}
			}

			if (lowerKey == 'e')
			{
				for (int i = word.Length - 1; i >= 0; i--)
				{
					int tone;
					int row = GetCharRow(word[i], out tone);
					if (row == 3)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[4][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 15)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[16][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 4)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[3][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 16)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[15][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
				}
			}

			if (lowerKey == 'o')
			{
				for (int i = word.Length - 1; i >= 0; i--)
				{
					int tone;
					int row = GetCharRow(word[i], out tone);
					if (row == 6 || row == 8)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[7][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 18 || row == 20)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[19][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 7)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[6][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 19)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[18][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
				}
			}

			if (lowerKey == 'w')
			{
				for (int i = 0; i < word.Length - 1; i++)
				{
					int toneU, toneO;
					int rowU = GetCharRow(word[i], out toneU);
					int rowO = GetCharRow(word[i + 1], out toneO);
					bool isU = (rowU == 9 || rowU == 10 || rowU == 21 || rowU == 22);
					bool isO = (rowO == 6 || rowO == 7 || rowO == 8 || rowO == 18 || rowO == 19 || rowO == 20);

					if (isU && isO)
					{
						int combinedTone = System.Math.Max(toneU, toneO);
						StringBuilder sb = new StringBuilder(word);
						sb[i] = (rowU >= 21) ? VOWEL_TABLE[22][0] : VOWEL_TABLE[10][0];
						sb[i + 1] = (rowO >= 18) ? VOWEL_TABLE[20][combinedTone] : VOWEL_TABLE[8][combinedTone];
						result = prefix + sb.ToString();
						return true;
					}
				}

				for (int i = word.Length - 1; i >= 0; i--)
				{
					int tone;
					int row = GetCharRow(word[i], out tone);

					if (row == 0)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[1][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 12)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[13][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 1)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[0][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 13)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[12][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}

					if (row == 6)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[8][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 18)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[20][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 8)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[6][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 20)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[18][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}

					if (row == 9)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[10][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 21)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[22][tone];
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 10)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[9][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
					if (row == 22)
					{
						StringBuilder sb = new StringBuilder(word);
						sb[i] = VOWEL_TABLE[21][tone];
						sb.Append(key);
						result = prefix + sb.ToString();
						return true;
					}
				}
			}

			return false;
		}
	}
}
