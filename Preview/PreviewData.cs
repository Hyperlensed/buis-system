#if GODOT && TOOLS
using Godot;
#endif

using System;
using System.Text;

namespace BuisSystem.Preview {
	public static class PreviewData {
#if GODOT && TOOLS
		private static Texture2D _previewImage = null;

		public static void Initialize(string addonsRootPath) {
			_previewImage = ResourceLoader.Load<Texture2D>(addonsRootPath.PathJoin("Preview/PreviewImage.svg"), "Texture2D");
		}
		public static void Uninitialize() {
			_previewImage.Free();
		}

		public static bool ApplyPreviewImageToTextureRect(TextureRect textureRect) {
			if (_previewImage != null) {
				textureRect.Texture = _previewImage;

				textureRect.ExpandMode = TextureRect.ExpandModeEnum.KeepSize;
				textureRect.StretchMode = TextureRect.StretchModeEnum.KeepAspectCovered;

				return true;
			}

			return false;
		}
#endif

		private static readonly string[] _loremIpsumWords = ["lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"];
		public static string GetLoremIpsumText(int targetCharacterCount = -1) {
			if (targetCharacterCount == 0) {
				return "";
			}

			if (targetCharacterCount < 0) {
				targetCharacterCount = new Random().Next(350, 451);
			}

			int characterCount = 0;
			int wordIndex = 0;
			StringBuilder sb = new StringBuilder();

			while (true) {
				string word = _loremIpsumWords[wordIndex % _loremIpsumWords.Length];
				int wordLength = word.Length;
				
				string nextWord = _loremIpsumWords[(wordIndex + 1) % _loremIpsumWords.Length];
				int nextWordLength = nextWord.Length;

				if ((characterCount + wordLength) <= targetCharacterCount) {
					if (wordIndex == 0) {
						sb.Append(word[0]);
						sb.Append(word.AsSpan(1));
					} else {
						sb.Append(word);
					}
				} else {
					break;
				}

				if ((characterCount + wordLength + 1 + nextWordLength) <= targetCharacterCount) {
					sb.Append(' ');
					characterCount++;
				}

				characterCount += wordLength;

				wordIndex++;
			}
			
			while (characterCount < targetCharacterCount) {
				sb.Append('.');
			}

			return sb.ToString();
		}
	}
}
