using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



	namespace Assets.Scripts.Models
	{
		class TransliterationModel
		{
			private static readonly Dictionary<Char, String> transliterationTemplate = new Dictionary<Char, String>
			{
				{'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "H"}, {'Ґ', "G"},
				{'Д', "D"}, {'Е', "E"}, {'Є', "Ye"}, {'Ж', "Zh"}, {'З', "Z"},
				{'И', "Y"}, {'І', "I"}, {'Ї', "Yi"}, {'Й', "Y"}, {'К', "K"},
				{'Л', "L"}, {'М', "M"}, {'Н', "N"}, {'О', "O"}, {'П', "P"},
				{'Р', "R"}, {'С', "S"}, {'Т', "T"}, {'У', "U"}, {'Ф', "F"},
				{'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Shch"},
				{'Ь', ""}, {'Ю', "Yu"}, {'Я', "Ya"}, {'ї', "i"}, {'й', "i"},
				{'ю', "iu"}, {'я',"ia"}

			};


			public static string Transliterate(string str)
			{
				string res = "";
				for (int i = 0; i < str.Length; i++)
				{
					if (transliterationTemplate.ContainsKey(str[i]))
					{
						res += transliterationTemplate[str[i]];
					}
					else if (transliterationTemplate.ContainsKey(Char.ToUpper(str[i])))
					{
						string c = transliterationTemplate[Char.ToUpper(str[i])];
						res += (Char.ToLower(c[0]) + c.Substring(1));
					}
					else
					{
						res += str[i];
					}
				}
				return res;
			}
		};

	}

