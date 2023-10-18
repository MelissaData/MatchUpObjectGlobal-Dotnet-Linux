// mdMatchUP.cs

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace MelissaData {
	public class mdMatchcodeComponent : IDisposable {
		public IntPtr i;

		public enum MatchcodeComponentType {
			PrefixComp = 1,
			FirstComp = 2,
			MiddleComp = 3,
			LastComp = 4,
			SuffixComp = 5,
			GenderComp = 6,
			FirstNicknameComp = 7,
			MiddleNicknameComp = 8,
			TitleComp = 9,
			CompanyComp = 10,
			CompanyAcronymComp = 11,
			StreetNumberComp = 12,
			StreetPreDirComp = 13,
			StreetNameComp = 14,
			StreetSuffixComp = 15,
			StreetPostDirComp = 16,
			POBoxComp = 17,
			SecondaryComp = 18,
			AddressComp = 19,
			CityComp = 20,
			StateComp = 21,
			Zip9Comp = 22,
			Zip5Comp = 23,
			Zip4Comp = 24,
			CountryComp = 28,
			CanadianPCComp = 29,
			UKCityComp = 30, // Obsolete
			UKCountyComp = 31, // Obsolete
			UKPCComp = 32, // Obsolete
			PhoneComp = 33,
			EMailComp = 34,
			CreditCardComp = 35,
			GeneralComp = 36,
			GeoDistanceComp = 38,
			DateComp = 39,
			NumericComp = 40,
			PremisesNumberComp = 41,
			ThoroughfarePreDirComp = 42,
			ThoroughfareLeadingTypeComp = 43,
			ThoroughfareNameComp = 44,
			ThoroughfarePostDirComp = 45,
			ThoroughfareTrailingTypeComp = 46,
			DepThoroughfarePreDirComp = 47,
			DepThoroughfareLeadingTypeComp = 48,
			DepThoroughfareNameComp = 49,
			DepThoroughfarePostDirComp = 50,
			DepThoroughfareTrailingTypeComp = 51,
			LocalityComp = 52,
			DependentLocalityComp = 53,
			DblDependentLocalityComp = 54,
			AdministrativeAreaComp = 55,
			SubAdministrativeAreaComp = 56,
			PostalCodeComp = 57,
			SubNationalAreaComp = 58,
			PostBoxComp = 59
		}
		[Flags]
		public enum MatchcodeStart {
			Left = 8,
			Right = 16,
			StartAtPos = 32,
			StartAtWord = 64
		}
		[Flags]
		public enum MatchcodeTrim {
			LeftTrim = 2,
			RightTrim = 4,
			AllTrim = 6
		}
		public enum MatchcodeFuzzy {
			Exact = 0,
			SoundEx = 1,
			Phonetex = 2,
			Containment = 4,
			Frequency = 8,
			FastNear = 16,
			AccurateNear = 32,
			VowelsOnly = 64,
			ConsonantsOnly = 128,
			AlphasOnly = 256,
			NumericsOnly = 512,
			FrequencyNear = 1024,
			NGram = 2048,
			Jaro = 4096,
			JaroWinkler = 8192,
			LCS = 16384,
			NeedlemanWunsch = 32768,
			MDKeyboard = 65536,
			SmithWatermanGotoh = 131072,
			Dice = 262144,
			Jaccard = 524288,
			Overlap = 1048576,
			DoubleMetaphone = 2097152,
			InternationalNear = 4194304
		}
		[Flags]
		public enum MatchcodeFieldMatch {
			NoFieldMatch = 0,
			BothBlankMatch = 256,
			OneBlankMatch = 512,
			InitialMatch = 1024
		}
		[Flags]
		public enum MatchcodeSwap {
			NoSwap = 0,
			SwapA = 1,
			SwapB = 2,
			SwapC = 4,
			SwapD = 8,
			SwapE = 16,
			SwapF = 32,
			SwapG = 64,
			SwapH = 128,
			BothA = 256,
			BothB = 512,
			BothC = 1024,
			BothD = 2048,
			BothE = 4096,
			BothF = 8192,
			BothG = 16384,
			BothH = 32768
		}
		[Flags]
		public enum MatchcodeCombination {
			Combo1 = 1,
			Combo2 = 2,
			Combo3 = 4,
			Combo4 = 8,
			Combo5 = 16,
			Combo6 = 32,
			Combo7 = 64,
			Combo8 = 128,
			Combo9 = 256,
			Combo10 = 512,
			Combo11 = 1024,
			Combo12 = 2048,
			Combo13 = 4096,
			Combo14 = 8192,
			Combo15 = 16384,
			Combo16 = 32768
		}
		public enum MatchcodeNearType {
			NearNone = 0,
			NearInteger = 1,
			NearFloat = 2
		}
		[Flags]
		public enum ComponentCountryType {
			US = 1,
			Canada = 2,
			Global = 4,
			Domestic = 3
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUMatchcodeComponentUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetComponentType(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetComponentType", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetComponentType(IntPtr i, Int32 Type);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSize", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetSize(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetSize", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetSize(IntPtr i, Int32 Size);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetLabel(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetLabel(IntPtr i, IntPtr Label);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetWordCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetWordCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetWordCount", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetWordCount(IntPtr i, Int32 WordCount);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetStart", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetStart(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetStart", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetStart(IntPtr i, Int32 Start);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetStartPos", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetStartPos(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetStartPos", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetStartPos(IntPtr i, Int32 StartPos);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetTrim", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetTrim(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetTrim", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetTrim(IntPtr i, Int32 Trim);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFuzzy", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetFuzzy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetFuzzy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetFuzzy(IntPtr i, Int32 Fuzzy);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetNear", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetNear(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetNear", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetNear(IntPtr i, Int32 Near);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetNearDbl", CallingConvention = CallingConvention.StdCall)]
			public static extern double mdMUMatchcodeComponentGetNearDbl(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetNearDbl", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetNearDbl(IntPtr i, double Near);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFieldMatch", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetFieldMatch(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetFieldMatch", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetFieldMatch(IntPtr i, Int32 Match);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSwap", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetSwap(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetSwap", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetSwap(IntPtr i, Int32 Swap);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetCombination", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetCombination(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetCombination", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetCombination(IntPtr i, Int32 Combination);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentCountryTypeFromEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetComponentCountryTypeFromEnum(IntPtr i, Int32 Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetComponentDescription(IntPtr i, Int32 UseLabel);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentDescriptionFromEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetComponentDescriptionFromEnum(IntPtr i, Int32 Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentAbbreviation", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetComponentAbbreviation(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseComponentDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseComponentDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSizeDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetSizeDescription(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseSizeDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseSizeDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseWordCountDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseWordCountDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetStartDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetStartDescription(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseStartDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseStartDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseStartPosDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseStartPosDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFuzzyDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetFuzzyDescription(IntPtr i, Int32 Decorated);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFuzzyDescriptionFromEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetFuzzyDescriptionFromEnum(IntPtr i, Int32 Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseFuzzyDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseFuzzyDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseNearDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern double mdMUMatchcodeComponentParseNearDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFieldMatchDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetFieldMatchDescription(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentParseFieldMatchDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentParseFieldMatchDescription(IntPtr i, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSwapDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetSwapDescription(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeComponentType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeComponentType(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeLabel(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeSize", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeSize(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeWordCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeWordCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeStart", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeStart(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeTrim", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeTrim(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentCanChangeFuzzy", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentCanChangeFuzzy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSizeMinimum", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetSizeMinimum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetSizeMaximum", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetSizeMaximum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetAllowedStarts", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetAllowedStarts(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetAllowedFuzzies", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetAllowedFuzzies(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentIsAllowedFuzzy", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentIsAllowedFuzzy(IntPtr i, Int32 Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFuzzyNearType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetFuzzyNearType(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetNearMinimum", CallingConvention = CallingConvention.StdCall)]
			public static extern double mdMUMatchcodeComponentGetNearMinimum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetNearMaximum", CallingConvention = CallingConvention.StdCall)]
			public static extern double mdMUMatchcodeComponentGetNearMaximum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetAllowedFieldMatches", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetAllowedFieldMatches(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetAllowedCombinations", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetAllowedCombinations(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetAllowedSwaps", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeComponentGetAllowedSwaps(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetComponentTypeEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetComponentTypeEnum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetFuzzyEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetFuzzyEnum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentSetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeComponentSetReserved(IntPtr i, IntPtr Property, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeComponentGetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeComponentGetReserved(IntPtr i, IntPtr Property);
		}

		internal mdMatchcodeComponent(IntPtr iCopy) {
			i = iCopy;
		}

		public mdMatchcodeComponent() {
			i = mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCreate();
		}

		~mdMatchcodeComponent() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public MatchcodeComponentType GetComponentType() {
			return (MatchcodeComponentType)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentType(i);
		}

		public void SetComponentType(MatchcodeComponentType Type) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetComponentType(i, (int)Type);
		}

		public int GetSize() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSize(i);
		}

		public void SetSize(int Size) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetSize(i, Size);
		}

		public string GetLabel() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetLabel(i));
		}

		public void SetLabel(string Label) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetLabel(i, (new Utf8String(Label)).GetUtf8Ptr());
		}

		public int GetWordCount() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetWordCount(i);
		}

		public void SetWordCount(int WordCount) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetWordCount(i, WordCount);
		}

		public MatchcodeStart GetStart() {
			return (MatchcodeStart)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetStart(i);
		}

		public void SetStart(MatchcodeStart Start) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetStart(i, (int)Start);
		}

		public int GetStartPos() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetStartPos(i);
		}

		public void SetStartPos(int StartPos) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetStartPos(i, StartPos);
		}

		public MatchcodeTrim GetTrim() {
			return (MatchcodeTrim)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetTrim(i);
		}

		public void SetTrim(MatchcodeTrim Trim) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetTrim(i, (int)Trim);
		}

		public MatchcodeFuzzy GetFuzzy() {
			return (MatchcodeFuzzy)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFuzzy(i);
		}

		public void SetFuzzy(MatchcodeFuzzy Fuzzy) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetFuzzy(i, (int)Fuzzy);
		}

		public int GetNear() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetNear(i);
		}

		public void SetNear(int Near) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetNear(i, Near);
		}

		public double GetNearDbl() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetNearDbl(i);
		}

		public void SetNearDbl(double Near) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetNearDbl(i, Near);
		}

		public MatchcodeFieldMatch GetFieldMatch() {
			return (MatchcodeFieldMatch)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFieldMatch(i);
		}

		public void SetFieldMatch(MatchcodeFieldMatch Match) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetFieldMatch(i, (int)Match);
		}

		public MatchcodeSwap GetSwap() {
			return (MatchcodeSwap)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSwap(i);
		}

		public void SetSwap(MatchcodeSwap Swap) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetSwap(i, (int)Swap);
		}

		public MatchcodeCombination GetCombination() {
			return (MatchcodeCombination)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetCombination(i);
		}

		public void SetCombination(MatchcodeCombination Combination) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetCombination(i, (int)Combination);
		}

		public ComponentCountryType GetComponentCountryTypeFromEnum(MatchcodeComponentType Value) {
			return (ComponentCountryType)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentCountryTypeFromEnum(i, (int)Value);
		}

		public string GetComponentDescription(int UseLabel) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentDescription(i,UseLabel));
		}

		public string GetComponentDescriptionFromEnum(MatchcodeComponentType Value) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentDescriptionFromEnum(i, (int)Value));
		}

		public string GetComponentAbbreviation() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentAbbreviation(i));
		}

		public MatchcodeComponentType ParseComponentDescription(string Value) {
			return (MatchcodeComponentType)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseComponentDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetSizeDescription() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSizeDescription(i));
		}

		public int ParseSizeDescription(string Value) {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseSizeDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public int ParseWordCountDescription(string Value) {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseWordCountDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetStartDescription() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetStartDescription(i));
		}

		public MatchcodeStart ParseStartDescription(string Value) {
			return (MatchcodeStart)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseStartDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public int ParseStartPosDescription(string Value) {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseStartPosDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetFuzzyDescription(int Decorated) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFuzzyDescription(i, Decorated));
		}

		public string GetFuzzyDescriptionFromEnum(MatchcodeFuzzy Value) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFuzzyDescriptionFromEnum(i, (int)Value));
		}

		public MatchcodeFuzzy ParseFuzzyDescription(string Value) {
			return (MatchcodeFuzzy)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseFuzzyDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public double ParseNearDescription(string Value) {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseNearDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetFieldMatchDescription() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFieldMatchDescription(i));
		}

		public MatchcodeFieldMatch ParseFieldMatchDescription(string Value) {
			return (MatchcodeFieldMatch)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentParseFieldMatchDescription(i, (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetSwapDescription() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSwapDescription(i));
		}

		public int CanChangeComponentType() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeComponentType(i);
		}

		public int CanChangeLabel() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeLabel(i);
		}

		public int CanChangeSize() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeSize(i);
		}

		public int CanChangeWordCount() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeWordCount(i);
		}

		public int CanChangeStart() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeStart(i);
		}

		public int CanChangeTrim() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeTrim(i);
		}

		public int CanChangeFuzzy() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentCanChangeFuzzy(i);
		}

		public int GetSizeMinimum() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSizeMinimum(i);
		}

		public int GetSizeMaximum() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetSizeMaximum(i);
		}

		public MatchcodeStart GetAllowedStarts() {
			return (MatchcodeStart)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetAllowedStarts(i);
		}

		public MatchcodeFuzzy GetAllowedFuzzies() {
			return (MatchcodeFuzzy)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetAllowedFuzzies(i);
		}

		public int IsAllowedFuzzy(MatchcodeFuzzy Value) {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentIsAllowedFuzzy(i, (int)Value);
		}

		public MatchcodeNearType GetFuzzyNearType() {
			return (MatchcodeNearType)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFuzzyNearType(i);
		}

		public double GetNearMinimum() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetNearMinimum(i);
		}

		public double GetNearMaximum() {
			return mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetNearMaximum(i);
		}

		public MatchcodeFieldMatch GetAllowedFieldMatches() {
			return (MatchcodeFieldMatch)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetAllowedFieldMatches(i);
		}

		public MatchcodeCombination GetAllowedCombinations() {
			return (MatchcodeCombination)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetAllowedCombinations(i);
		}

		public MatchcodeSwap GetAllowedSwaps() {
			return (MatchcodeSwap)mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetAllowedSwaps(i);
		}

		public string GetComponentTypeEnum() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetComponentTypeEnum(i));
		}

		public string GetFuzzyEnum() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetFuzzyEnum(i));
		}

		public void SetReserved(string Property, string Value) {
			mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentSetReserved(i, (new Utf8String(Property)).GetUtf8Ptr(), (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetReserved(string Property) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeComponentUnmanaged.mdMUMatchcodeComponentGetReserved(i, (new Utf8String(Property)).GetUtf8Ptr()));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdMatchcodeList : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorConfigFile = 1,
			ErrorLicenseExpired = 2,
			ErrorDatabaseExpired = 3,
			ErrorMatchcodeNotSpecified = 4,
			ErrorMatchcodeNotFound = 5,
			ErrorInvalidMatchcode = 6,
			ErrorKeyFile = 7,
			ErrorNoneIntersectingGroupCache = 8,
			ErrorMatchcodeObsolete = 9,
			ErrorGlobalAddrInit = 10,
			ErrorIntlLicenseRequired = 11
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUMatchcodeListUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeListCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeListDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListSetPathToMatchUpFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeListSetPathToMatchUpFiles(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListInitializeDataFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeListInitializeDataFiles(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListGetInitializeErrorString", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeListGetInitializeErrorString(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListGetMatchcodeCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeListGetMatchcodeCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeListGetMatchcodeName", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeListGetMatchcodeName(IntPtr i, Int32 Pos);
		}

		public mdMatchcodeList() {
			i = mdMUMatchcodeListUnmanaged.mdMUMatchcodeListCreate();
		}

		~mdMatchcodeList() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUMatchcodeListUnmanaged.mdMUMatchcodeListDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToMatchUpFiles(string Path) {
			mdMUMatchcodeListUnmanaged.mdMUMatchcodeListSetPathToMatchUpFiles(i, (new Utf8String(Path)).GetUtf8Ptr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdMUMatchcodeListUnmanaged.mdMUMatchcodeListInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeListUnmanaged.mdMUMatchcodeListGetInitializeErrorString(i));
		}

		public int GetMatchcodeCount() {
			return mdMUMatchcodeListUnmanaged.mdMUMatchcodeListGetMatchcodeCount(i);
		}

		public string GetMatchcodeName(int Pos) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeListUnmanaged.mdMUMatchcodeListGetMatchcodeName(i, Pos));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdMatchcode : IDisposable {
		public IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorConfigFile = 1,
			ErrorLicenseExpired = 2,
			ErrorDatabaseExpired = 3,
			ErrorMatchcodeNotSpecified = 4,
			ErrorMatchcodeNotFound = 5,
			ErrorInvalidMatchcode = 6,
			ErrorKeyFile = 7,
			ErrorNoneIntersectingGroupCache = 8,
			ErrorMatchcodeObsolete = 9,
			ErrorGlobalAddrInit = 10,
			ErrorIntlLicenseRequired = 11
		}
		public enum MatchcodeMappingTarget {
			PrefixType = 1,
			FirstType = 2,
			MiddleType = 3,
			LastType = 4,
			SuffixType = 5,
			GenderType = 6,
			FirstNicknameType = 7,
			MiddleNicknameType = 8,
			TitleType = 9,
			CompanyType = 10,
			CompanyAcronymType = 11,
			AddressType = 12,
			CityType = 13,
			StateType = 14,
			Zip9Type = 15,
			Zip5Type = 16,
			Zip4Type = 17,
			CountryType = 18,
			CanadianPCType = 19,
			UKCityType = 20, // Obsolete
			UKCountyType = 21, // Obsolete
			UKPCType = 22, // Obsolete
			PhoneType = 23,
			EMailType = 24,
			CreditCardType = 25,
			GeneralType = 26,
			Address1Type = 28,
			Address2Type = 29,
			Address3Type = 30,
			LatitudeType = 34,
			LongitudeType = 35,
			DateType = 36,
			NumericType = 37,
			Address4Type = 38,
			Address5Type = 39,
			Address6Type = 40,
			Address7Type = 41,
			Address8Type = 42
		}
		public enum MatchcodeMapping {
			Prefix = 1,
			Gender = 2,
			First = 3,
			MixedFirst = 4,
			Middle = 5,
			Last = 6,
			MixedLast = 7,
			Suffix = 8,
			FullName = 9,
			InverseName = 10,
			GovernmentInverseName = 11,
			Title = 12,
			Company = 13,
			Address = 14,
			City = 15,
			State = 16,
			Zip9 = 17,
			Zip5 = 18,
			Zip4 = 19,
			CityStZip = 20,
			Country = 21,
			CanadianPostalCode = 22,
			UKCity = 23, // Obsolete
			UKCounty = 24, // Obsolete
			UKPostcode = 25, // Obsolete
			UKCityCountyPC = 26, // Obsolete
			Phone = 27,
			EMail = 28,
			CreditCard = 29,
			General = 30,
			Latitude = 40,
			Longitude = 41,
			Date = 42,
			Numeric = 43
		}
		public enum MatchcodeStatus {
			MCNoError = 0,
			MCFirstComponentFuzzyOptions = 1,
			MCFirstComponentNoSwapPair = 2,
			MCDataTypeNoFuzzy = 3,
			MCComponentFuzzyIncorrectSize = 4,
			MCDataTypeNoMaximumNumberWords = 5,
			MCDataTypeNoStartRightOrWordOrPos = 6,
			MCIncorrectMaximumNumberWords = 7,
			MCNearOutOfRange = 8,
			MCFirstComponentNotUsedInEveryCondition = 9,
			MCCannotChangeFirstComponent = 10,
			MCInvalidSwapPair = 11,
			MCIncorrectStartPosOrStartWord = 12,
			MCInvalidMatchcodeComponentType = 13
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUMatchcodeUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSetPathToMatchUpFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSetPathToMatchUpFiles(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeInitializeDataFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeInitializeDataFiles(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetInitializeErrorString", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetInitializeErrorString(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeFindMatchcode", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeFindMatchcode(IntPtr i, IntPtr Matchcode);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMatchcodeName", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetMatchcodeName(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSetDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSetDescription(IntPtr i, IntPtr Description);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetDescription(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSetNGram", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSetNGram(IntPtr i, Int32 NGram);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetNGram", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetNGram(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMatchcodeItemCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetMatchcodeItemCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMatchcodeItem", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetMatchcodeItem(IntPtr i, Int32 Pos);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMappingItemCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetMappingItemCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMappingItemType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetMappingItemType(IntPtr i, Int32 Pos);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMappingItemLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetMappingItemLabel(IntPtr i, Int32 Pos);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeAddMatchcodeItem", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeAddMatchcodeItem(IntPtr i, IntPtr Item);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeInsertMatchcodeItem", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeInsertMatchcodeItem(IntPtr i, Int32 Pos, IntPtr Item);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeChangeMatchcodeItem", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeChangeMatchcodeItem(IntPtr i, Int32 Pos, IntPtr Item);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeDeleteMatchcodeItem", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeDeleteMatchcodeItem(IntPtr i, Int32 Pos);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeRenameMatchcode", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeRenameMatchcode(IntPtr i, IntPtr Name);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeDeleteMatchcode", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeDeleteMatchcode(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSave", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSave(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSaveToFile", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSaveToFile(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeCreateNewMatchcode", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeCreateNewMatchcode(IntPtr i, IntPtr Matchcode);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetRuleDescription", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetRuleDescription(IntPtr i, Int32 Rule_, Int32 Abbreviated_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetMaximumCombinations", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetMaximumCombinations(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetAllowedInputMappingCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetAllowedInputMappingCount(IntPtr i, Int32 Mapping_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetAllowedInputMappingType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetAllowedInputMappingType(IntPtr i, Int32 Mapping_, Int32 Pos_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetAllowedInputMappingLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetAllowedInputMappingLabel(IntPtr i, Int32 Mapping_, Int32 Pos_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetInputMappingLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetInputMappingLabel(IntPtr i, Int32 Mapping_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeParseInputMappingLabel", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeParseInputMappingLabel(IntPtr i, IntPtr Value_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetBestInputMappingType", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeGetBestInputMappingType(IntPtr i, Int32 Target_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeIsDirectConversion", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeIsDirectConversion(IntPtr i, Int32 Source_, Int32 Target_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeIsConvertable", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUMatchcodeIsConvertable(IntPtr i, Int32 Source_, Int32 Target_);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetInputMappingEnum", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetInputMappingEnum(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeSetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUMatchcodeSetReserved(IntPtr i, IntPtr Property, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUMatchcodeGetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUMatchcodeGetReserved(IntPtr i, IntPtr Property);
		}

		public mdMatchcode() {
			i = mdMUMatchcodeUnmanaged.mdMUMatchcodeCreate();
		}

		internal mdMatchcode(IntPtr iCopy) {
			i = iCopy;
		}

		~mdMatchcode() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUMatchcodeUnmanaged.mdMUMatchcodeDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToMatchUpFiles(string Path) {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSetPathToMatchUpFiles(i, (new Utf8String(Path)).GetUtf8Ptr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdMUMatchcodeUnmanaged.mdMUMatchcodeInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetInitializeErrorString(i));
		}

		public int FindMatchcode(string Matchcode) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeFindMatchcode(i, (new Utf8String(Matchcode)).GetUtf8Ptr());
		}

		public string GetMatchcodeName() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMatchcodeName(i));
		}

		public void SetDescription(string Description) {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSetDescription(i, (new Utf8String(Description)).GetUtf8Ptr());
		}

		public string GetDescription() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetDescription(i));
		}

		public void SetNGram(int NGram) {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSetNGram(i, NGram);
		}

		public int GetNGram() {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeGetNGram(i);
		}

		public int GetMatchcodeItemCount() {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMatchcodeItemCount(i);
		}

		public mdMatchcodeComponent GetMatchcodeItem(int Pos) {
			IntPtr retVal = mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMatchcodeItem(i, Pos);
			if (retVal == null)
				return null;
			return new mdMatchcodeComponent(retVal);
		}

		public int GetMappingItemCount() {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMappingItemCount(i);
		}

		public MatchcodeMappingTarget GetMappingItemType(int Pos) {
			return (MatchcodeMappingTarget)mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMappingItemType(i, Pos);
		}

		public string GetMappingItemLabel(int Pos) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMappingItemLabel(i, Pos));
		}

		public MatchcodeStatus AddMatchcodeItem(mdMatchcodeComponent Item) {
			return (MatchcodeStatus)mdMUMatchcodeUnmanaged.mdMUMatchcodeAddMatchcodeItem(i, Item.i);
		}

		public MatchcodeStatus InsertMatchcodeItem(int Pos, mdMatchcodeComponent Item) {
			return (MatchcodeStatus)mdMUMatchcodeUnmanaged.mdMUMatchcodeInsertMatchcodeItem(i, Pos, Item.i);
		}

		public MatchcodeStatus ChangeMatchcodeItem(int Pos, mdMatchcodeComponent Item) {
			return (MatchcodeStatus)mdMUMatchcodeUnmanaged.mdMUMatchcodeChangeMatchcodeItem(i, Pos, Item.i);
		}

		public MatchcodeStatus DeleteMatchcodeItem(int Pos) {
			return (MatchcodeStatus)mdMUMatchcodeUnmanaged.mdMUMatchcodeDeleteMatchcodeItem(i, Pos);
		}

		public int RenameMatchcode(string Name) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeRenameMatchcode(i, (new Utf8String(Name)).GetUtf8Ptr());
		}

		public int DeleteMatchcode() {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeDeleteMatchcode(i);
		}

		public void Save() {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSave(i);
		}

		public void SaveToFile(string Path) {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSaveToFile(i, (new Utf8String(Path)).GetUtf8Ptr());
		}

		public int CreateNewMatchcode(string Matchcode) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeCreateNewMatchcode(i, (new Utf8String(Matchcode)).GetUtf8Ptr());
		}

		public string GetRuleDescription(int Rule_, int Abbreviated_) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetRuleDescription(i, Rule_, Abbreviated_));
		}

		public int GetMaximumCombinations() {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeGetMaximumCombinations(i);
		}

		public int GetAllowedInputMappingCount(MatchcodeMappingTarget Mapping_) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeGetAllowedInputMappingCount(i, (int)Mapping_);
		}

		public MatchcodeMapping GetAllowedInputMappingType(MatchcodeMappingTarget Mapping_, int Pos_) {
			return (MatchcodeMapping)mdMUMatchcodeUnmanaged.mdMUMatchcodeGetAllowedInputMappingType(i, (int)Mapping_, Pos_);
		}

		public string GetAllowedInputMappingLabel(MatchcodeMappingTarget Mapping_, int Pos_) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetAllowedInputMappingLabel(i, (int)Mapping_, Pos_));
		}

		public string GetInputMappingLabel(MatchcodeMapping Mapping_) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetInputMappingLabel(i, (int)Mapping_));
		}

		public MatchcodeMapping ParseInputMappingLabel(string Value_) {
			return (MatchcodeMapping)mdMUMatchcodeUnmanaged.mdMUMatchcodeParseInputMappingLabel(i, (new Utf8String(Value_)).GetUtf8Ptr());
		}

		public MatchcodeMapping GetBestInputMappingType(MatchcodeMappingTarget Target_) {
			return (MatchcodeMapping)mdMUMatchcodeUnmanaged.mdMUMatchcodeGetBestInputMappingType(i, (int)Target_);
		}

		public int IsDirectConversion(MatchcodeMapping Source_, MatchcodeMappingTarget Target_) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeIsDirectConversion(i, (int)Source_, (int)Target_);
		}

		public int IsConvertable(MatchcodeMapping Source_, MatchcodeMappingTarget Target_) {
			return mdMUMatchcodeUnmanaged.mdMUMatchcodeIsConvertable(i, (int)Source_, (int)Target_);
		}

		public string GetInputMappingEnum() {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetInputMappingEnum(i));
		}

		public void SetReserved(string Property, string Value) {
			mdMUMatchcodeUnmanaged.mdMUMatchcodeSetReserved(i, (new Utf8String(Property)).GetUtf8Ptr(), (new Utf8String(Value)).GetUtf8Ptr());
		}

		public string GetReserved(string Property) {
			return Utf8String.GetUnicodeString(mdMUMatchcodeUnmanaged.mdMUMatchcodeGetReserved(i, (new Utf8String(Property)).GetUtf8Ptr()));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdReadWrite : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorConfigFile = 1,
			ErrorLicenseExpired = 2,
			ErrorDatabaseExpired = 3,
			ErrorMatchcodeNotSpecified = 4,
			ErrorMatchcodeNotFound = 5,
			ErrorInvalidMatchcode = 6,
			ErrorKeyFile = 7,
			ErrorNoneIntersectingGroupCache = 8,
			ErrorMatchcodeObsolete = 9,
			ErrorGlobalAddrInit = 10,
			ErrorIntlLicenseRequired = 11
		}
		public enum MatchcodeMapping {
			Prefix = 1,
			Gender = 2,
			First = 3,
			MixedFirst = 4,
			Middle = 5,
			Last = 6,
			MixedLast = 7,
			Suffix = 8,
			FullName = 9,
			InverseName = 10,
			GovernmentInverseName = 11,
			Title = 12,
			Company = 13,
			Address = 14,
			City = 15,
			State = 16,
			Zip9 = 17,
			Zip5 = 18,
			Zip4 = 19,
			CityStZip = 20,
			Country = 21,
			CanadianPostalCode = 22,
			UKCity = 23, // Obsolete
			UKCounty = 24, // Obsolete
			UKPostcode = 25, // Obsolete
			UKCityCountyPC = 26, // Obsolete
			Phone = 27,
			EMail = 28,
			CreditCard = 29,
			General = 30,
			Latitude = 40,
			Longitude = 41,
			Date = 42,
			Numeric = 43
		}
		[Flags]
		public enum MatchcodeCombination {
			Combo1 = 1,
			Combo2 = 2,
			Combo3 = 4,
			Combo4 = 8,
			Combo5 = 16,
			Combo6 = 32,
			Combo7 = 64,
			Combo8 = 128,
			Combo9 = 256,
			Combo10 = 512,
			Combo11 = 1024,
			Combo12 = 2048,
			Combo13 = 4096,
			Combo14 = 8192,
			Combo15 = 16384,
			Combo16 = 32768
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUReadWriteUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetPathToMatchUpFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetPathToMatchUpFiles(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetMatchcodeName", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetMatchcodeName(IntPtr i, IntPtr MatchcodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetMatchcodeObject(IntPtr i, IntPtr Matchcode);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetKeyFile", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetKeyFile(IntPtr i, IntPtr KeyFile);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteInitializeDataFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteInitializeDataFiles(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetInitializeErrorString", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetInitializeErrorString(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetLicenseString", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteSetLicenseString(IntPtr i, IntPtr License);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetMaximumCharacterSize", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetMaximumCharacterSize(IntPtr i, Int32 size);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetEncoding", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteSetEncoding(IntPtr i, IntPtr encodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetBuildNumber", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetBuildNumber(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetDatabaseDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetDatabaseDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetDatabaseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetDatabaseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetLicenseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetMatchcodeObject(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteClearMappings", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteClearMappings(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteAddMapping", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteAddMapping(IntPtr i, Int32 Mapping);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteClearFields", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteClearFields(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteAddField", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteAddField(IntPtr i, IntPtr Field);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteBuildKey", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteBuildKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetKey", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetKey(IntPtr i, IntPtr Key);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetUserInfo", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetUserInfo(IntPtr i, IntPtr UserInfo);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteWriteRecord", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteWriteRecord(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteProcess", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteProcess(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteReadRecord", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteReadRecord(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetKey", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetKeySize", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetKeySize(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetUserInfo", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetUserInfo(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetDupeGroup", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetDupeGroup(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetStatusCode", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetStatusCode(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetEntry", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetEntry(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetError", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetError(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetCombinations", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUReadWriteGetCombinations(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetResults", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetResults(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetGroupSorting", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetGroupSorting(IntPtr i, Int32 bGroupSorting);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteSetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUReadWriteSetReserved(IntPtr i, IntPtr Property, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUReadWriteGetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUReadWriteGetReserved(IntPtr i, IntPtr Property);
		}

		public mdReadWrite() {
			i = mdMUReadWriteUnmanaged.mdMUReadWriteCreate();
			EncodedString.SetLegacyUsage(true);
		}

		~mdReadWrite() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUReadWriteUnmanaged.mdMUReadWriteDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToMatchUpFiles(string Path) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetPathToMatchUpFiles(i, (new EncodedString(Path)).GetEncodedPtr());
		}

		public void SetMatchcodeName(string MatchcodeName) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetMatchcodeName(i, (new EncodedString(MatchcodeName)).GetEncodedPtr());
		}

		public void SetMatchcodeObject(mdMatchcode Matchcode) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetMatchcodeObject(i, Matchcode.i);
		}

		public void SetKeyFile(string KeyFile) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetKeyFile(i, (new EncodedString(KeyFile)).GetEncodedPtr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdMUReadWriteUnmanaged.mdMUReadWriteInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetInitializeErrorString(i));
		}

		public int SetLicenseString(string License) {
			return mdMUReadWriteUnmanaged.mdMUReadWriteSetLicenseString(i, (new EncodedString(License)).GetEncodedPtr());
		}

		public void SetMaximumCharacterSize(int size) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetMaximumCharacterSize(i, size);
		}

		public int SetEncoding(string encodeName) {
      if (string.Compare(encodeName, "UTF-8", true) == 0)
    	  EncodedString.SetLegacyUsage(false);
			return mdMUReadWriteUnmanaged.mdMUReadWriteSetEncoding(i, (new EncodedString(encodeName)).GetEncodedPtr());
		}

		public string GetBuildNumber() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetDatabaseExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetLicenseExpirationDate(i));
		}

		public mdMatchcode GetMatchcodeObject() {
			IntPtr retVal = mdMUReadWriteUnmanaged.mdMUReadWriteGetMatchcodeObject(i);
			if (retVal == null)
				return null;
			return new mdMatchcode(retVal);
		}

		public void ClearMappings() {
			mdMUReadWriteUnmanaged.mdMUReadWriteClearMappings(i);
		}

		public int AddMapping(MatchcodeMapping Mapping) {
			return mdMUReadWriteUnmanaged.mdMUReadWriteAddMapping(i, (int)Mapping);
		}

		public void ClearFields() {
			mdMUReadWriteUnmanaged.mdMUReadWriteClearFields(i);
		}

		public void AddField(string Field) {
			if (EncodedString.GetLegacyUsage())
				SetReserved("SkipMBSConvert", "T");
			mdMUReadWriteUnmanaged.mdMUReadWriteAddField(i, (new EncodedString(Field)).GetEncodedPtr());
		}

		public void BuildKey() {
			mdMUReadWriteUnmanaged.mdMUReadWriteBuildKey(i);
		}

		public void SetKey(string Key) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetKey(i, (new EncodedString(Key)).GetEncodedPtr());
		}

		public void SetUserInfo(string UserInfo) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetUserInfo(i, (new EncodedString(UserInfo)).GetEncodedPtr());
		}

		public void WriteRecord() {
			mdMUReadWriteUnmanaged.mdMUReadWriteWriteRecord(i);
		}

		public void Process() {
			mdMUReadWriteUnmanaged.mdMUReadWriteProcess(i);
		}

		public int ReadRecord() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteReadRecord(i);
		}

		public string GetKey() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetKey(i));
		}

		public int GetKeySize() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteGetKeySize(i);
		}

		public string GetUserInfo() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetUserInfo(i));
		}

		public int GetDupeGroup() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteGetDupeGroup(i);
		}

		public string GetStatusCode() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetStatusCode(i));
		}

		public int GetCount() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteGetCount(i);
		}

		public int GetEntry() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteGetEntry(i);
		}

		public int GetError() {
			return mdMUReadWriteUnmanaged.mdMUReadWriteGetError(i);
		}

		public MatchcodeCombination GetCombinations() {
			return (MatchcodeCombination)mdMUReadWriteUnmanaged.mdMUReadWriteGetCombinations(i);
		}

		public string GetResults() {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetResults(i));
		}

		public void SetGroupSorting(bool bGroupSorting) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetGroupSorting(i, (bGroupSorting ? 1 : 0));
		}

		public void SetReserved(string Property, string Value) {
			mdMUReadWriteUnmanaged.mdMUReadWriteSetReserved(i, (new EncodedString(Property)).GetEncodedPtr(), (new EncodedString(Value)).GetEncodedPtr());
		}

		public string GetReserved(string Property) {
			return EncodedString.GetUnicodeString(mdMUReadWriteUnmanaged.mdMUReadWriteGetReserved(i, (new EncodedString(Property)).GetEncodedPtr()));
		}

		private class EncodedString : IDisposable {
			private IntPtr encodedString = IntPtr.Zero;
			private static bool isLegacy = false;

			public static void SetLegacyUsage(bool legacy) {
				isLegacy = legacy;
			}
			public static bool GetLegacyUsage() {
				return isLegacy;
			}

			public EncodedString(string str) {
				Encoding encoding;
				if (isLegacy)
					encoding = Encoding.GetEncoding("ISO-8859-1");
				else
					encoding = Encoding.UTF8;

				if (str == null)
					str = "";

				byte[] buffer = encoding.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				encodedString = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, encodedString, buffer.Length);
			}

			~EncodedString() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(encodedString);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetEncodedPtr() {
				return encodedString;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);

				if (isLegacy)
					return Encoding.GetEncoding("ISO-8859-1").GetString(buffer);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdIncremental : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorConfigFile = 1,
			ErrorLicenseExpired = 2,
			ErrorDatabaseExpired = 3,
			ErrorMatchcodeNotSpecified = 4,
			ErrorMatchcodeNotFound = 5,
			ErrorInvalidMatchcode = 6,
			ErrorKeyFile = 7,
			ErrorNoneIntersectingGroupCache = 8,
			ErrorMatchcodeObsolete = 9,
			ErrorGlobalAddrInit = 10,
			ErrorIntlLicenseRequired = 11
		}
		public enum MatchcodeMapping {
			Prefix = 1,
			Gender = 2,
			First = 3,
			MixedFirst = 4,
			Middle = 5,
			Last = 6,
			MixedLast = 7,
			Suffix = 8,
			FullName = 9,
			InverseName = 10,
			GovernmentInverseName = 11,
			Title = 12,
			Company = 13,
			Address = 14,
			City = 15,
			State = 16,
			Zip9 = 17,
			Zip5 = 18,
			Zip4 = 19,
			CityStZip = 20,
			Country = 21,
			CanadianPostalCode = 22,
			UKCity = 23, // Obsolete
			UKCounty = 24, // Obsolete
			UKPostcode = 25, // Obsolete
			UKCityCountyPC = 26, // Obsolete
			Phone = 27,
			EMail = 28,
			CreditCard = 29,
			General = 30,
			Latitude = 40,
			Longitude = 41,
			Date = 42,
			Numeric = 43
		}
		[Flags]
		public enum MatchcodeCombination {
			Combo1 = 1,
			Combo2 = 2,
			Combo3 = 4,
			Combo4 = 8,
			Combo5 = 16,
			Combo6 = 32,
			Combo7 = 64,
			Combo8 = 128,
			Combo9 = 256,
			Combo10 = 512,
			Combo11 = 1024,
			Combo12 = 2048,
			Combo13 = 4096,
			Combo14 = 8192,
			Combo15 = 16384,
			Combo16 = 32768
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUIncrementalUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetPathToMatchUpFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetPathToMatchUpFiles(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetMatchcodeName", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetMatchcodeName(IntPtr i, IntPtr MatchcodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetMatchcodeObject(IntPtr i, IntPtr Matchcode);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetMustExist", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetMustExist(IntPtr i, Int32 bMustExist);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetKeyFile", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetKeyFile(IntPtr i, IntPtr KeyFile);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalInitializeDataFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalInitializeDataFiles(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetInitializeErrorString", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetInitializeErrorString(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetLicenseString", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalSetLicenseString(IntPtr i, IntPtr License);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetMaximumCharacterSize", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetMaximumCharacterSize(IntPtr i, Int32 size);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetEncoding", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalSetEncoding(IntPtr i, IntPtr encodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetBuildNumber", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetBuildNumber(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetDatabaseDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetDatabaseDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetDatabaseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetDatabaseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetLicenseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetMatchcodeObject(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalClearMappings", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalClearMappings(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalAddMapping", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalAddMapping(IntPtr i, Int32 Mapping);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalClearFields", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalClearFields(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalAddField", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalAddField(IntPtr i, IntPtr Field);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalBuildKey", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalBuildKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetKey", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetKey(IntPtr i, IntPtr Key);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetUserInfo", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetUserInfo(IntPtr i, IntPtr UserInfo);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalMatchRecord", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalMatchRecord(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalAddRecord", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalAddRecord(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalNextMatchingRecord", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalNextMatchingRecord(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetKey", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetUserInfo", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetUserInfo(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetDupeGroup", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalGetDupeGroup(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetStatusCode", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetStatusCode(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetCount", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalGetCount(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetEntry", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalGetEntry(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetCombinations", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalGetCombinations(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetResults", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetResults(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalSetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUIncrementalSetReserved(IntPtr i, IntPtr Property, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalGetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUIncrementalGetReserved(IntPtr i, IntPtr Property);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalBeginTransaction", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalBeginTransaction(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalCommitTransaction", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalCommitTransaction(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUIncrementalRollbackTransaction", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUIncrementalRollbackTransaction(IntPtr i);
		}

		public mdIncremental() {
			i = mdMUIncrementalUnmanaged.mdMUIncrementalCreate();
			EncodedString.SetLegacyUsage(true);
		}

		~mdIncremental() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUIncrementalUnmanaged.mdMUIncrementalDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToMatchUpFiles(string Path) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetPathToMatchUpFiles(i, (new EncodedString(Path)).GetEncodedPtr());
		}

		public void SetMatchcodeName(string MatchcodeName) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetMatchcodeName(i, (new EncodedString(MatchcodeName)).GetEncodedPtr());
		}

		public void SetMatchcodeObject(IntPtr Matchcode) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetMatchcodeObject(i, Matchcode);
		}

		public void SetMustExist(bool bMustExist) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetMustExist(i, (bMustExist ? 1 : 0));
		}

		public void SetKeyFile(string KeyFile) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetKeyFile(i, (new EncodedString(KeyFile)).GetEncodedPtr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdMUIncrementalUnmanaged.mdMUIncrementalInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetInitializeErrorString(i));
		}

		public int SetLicenseString(string License) {
			return mdMUIncrementalUnmanaged.mdMUIncrementalSetLicenseString(i, (new EncodedString(License)).GetEncodedPtr());
		}

		public void SetMaximumCharacterSize(int size) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetMaximumCharacterSize(i, size);
		}

		public int SetEncoding(string encodeName) {
      if (string.Compare(encodeName, "UTF-8", true) == 0)
    	  EncodedString.SetLegacyUsage(false);
			return mdMUIncrementalUnmanaged.mdMUIncrementalSetEncoding(i, (new EncodedString(encodeName)).GetEncodedPtr());
		}

		public string GetBuildNumber() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetDatabaseExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetLicenseExpirationDate(i));
		}

		public mdMatchcode GetMatchcodeObject() {
			IntPtr retVal = mdMUIncrementalUnmanaged.mdMUIncrementalGetMatchcodeObject(i);
			if (retVal == null)
				return null;
			return new mdMatchcode(retVal);
		}

		public void ClearMappings() {
			mdMUIncrementalUnmanaged.mdMUIncrementalClearMappings(i);
		}

		public int AddMapping(MatchcodeMapping Mapping) {
			return mdMUIncrementalUnmanaged.mdMUIncrementalAddMapping(i, (int)Mapping);
		}

		public void ClearFields() {
			mdMUIncrementalUnmanaged.mdMUIncrementalClearFields(i);
		}

		public void AddField(string Field) {
			if (EncodedString.GetLegacyUsage())
				SetReserved("SkipMBSConvert", "T");
			mdMUIncrementalUnmanaged.mdMUIncrementalAddField(i, (new EncodedString(Field)).GetEncodedPtr());
		}

		public void BuildKey() {
			mdMUIncrementalUnmanaged.mdMUIncrementalBuildKey(i);
		}

		public void SetKey(string Key) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetKey(i, (new EncodedString(Key)).GetEncodedPtr());
		}

		public void SetUserInfo(string UserInfo) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetUserInfo(i, (new EncodedString(UserInfo)).GetEncodedPtr());
		}

		public void MatchRecord() {
			mdMUIncrementalUnmanaged.mdMUIncrementalMatchRecord(i);
		}

		public void AddRecord() {
			mdMUIncrementalUnmanaged.mdMUIncrementalAddRecord(i);
		}

		public int NextMatchingRecord() {
			return mdMUIncrementalUnmanaged.mdMUIncrementalNextMatchingRecord(i);
		}

		public string GetKey() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetKey(i));
		}

		public string GetUserInfo() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetUserInfo(i));
		}

		public int GetDupeGroup() {
			return mdMUIncrementalUnmanaged.mdMUIncrementalGetDupeGroup(i);
		}

		public string GetStatusCode() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetStatusCode(i));
		}

		public int GetCount() {
			return mdMUIncrementalUnmanaged.mdMUIncrementalGetCount(i);
		}

		public int GetEntry() {
			return mdMUIncrementalUnmanaged.mdMUIncrementalGetEntry(i);
		}

		public int GetCombinations() {
			return mdMUIncrementalUnmanaged.mdMUIncrementalGetCombinations(i);
		}

		public string GetResults() {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetResults(i));
		}

		public void SetReserved(string Property, string Value) {
			mdMUIncrementalUnmanaged.mdMUIncrementalSetReserved(i, (new EncodedString(Property)).GetEncodedPtr(), (new EncodedString(Value)).GetEncodedPtr());
		}

		public string GetReserved(string Property) {
			return EncodedString.GetUnicodeString(mdMUIncrementalUnmanaged.mdMUIncrementalGetReserved(i, (new EncodedString(Property)).GetEncodedPtr()));
		}

		public bool BeginTransaction() {
			return (mdMUIncrementalUnmanaged.mdMUIncrementalBeginTransaction(i) != 0);
		}

		public bool CommitTransaction() {
			return (mdMUIncrementalUnmanaged.mdMUIncrementalCommitTransaction(i) != 0);
		}

		public bool RollbackTransaction() {
			return (mdMUIncrementalUnmanaged.mdMUIncrementalRollbackTransaction(i) != 0);
		}

		private class EncodedString : IDisposable {
			private IntPtr encodedString = IntPtr.Zero;
			private static bool isLegacy = false;

			public static void SetLegacyUsage(bool legacy) {
				isLegacy = legacy;
			}

			public static bool GetLegacyUsage() {
				return isLegacy;
			}

			public EncodedString(string str) {
				Encoding encoding;
				if (isLegacy)
					encoding = Encoding.GetEncoding("ISO-8859-1");
				else
					encoding = Encoding.UTF8;

				if (str == null)
					str = "";

				byte[] buffer = encoding.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				encodedString = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, encodedString, buffer.Length);
			}

			~EncodedString() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(encodedString);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetEncodedPtr() {
				return encodedString;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);

				if (isLegacy)
					return Encoding.GetEncoding("ISO-8859-1").GetString(buffer);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdHybrid : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorConfigFile = 1,
			ErrorLicenseExpired = 2,
			ErrorDatabaseExpired = 3,
			ErrorMatchcodeNotSpecified = 4,
			ErrorMatchcodeNotFound = 5,
			ErrorInvalidMatchcode = 6,
			ErrorKeyFile = 7,
			ErrorNoneIntersectingGroupCache = 8,
			ErrorMatchcodeObsolete = 9,
			ErrorGlobalAddrInit = 10,
			ErrorIntlLicenseRequired = 11
		}
		public enum MatchcodeMapping {
			Prefix = 1,
			Gender = 2,
			First = 3,
			MixedFirst = 4,
			Middle = 5,
			Last = 6,
			MixedLast = 7,
			Suffix = 8,
			FullName = 9,
			InverseName = 10,
			GovernmentInverseName = 11,
			Title = 12,
			Company = 13,
			Address = 14,
			City = 15,
			State = 16,
			Zip9 = 17,
			Zip5 = 18,
			Zip4 = 19,
			CityStZip = 20,
			Country = 21,
			CanadianPostalCode = 22,
			UKCity = 23, // Obsolete
			UKCounty = 24, // Obsolete
			UKPostcode = 25, // Obsolete
			UKCityCountyPC = 26, // Obsolete
			Phone = 27,
			EMail = 28,
			CreditCard = 29,
			General = 30,
			Latitude = 40,
			Longitude = 41,
			Date = 42,
			Numeric = 43
		}
		[Flags]
		public enum MatchcodeCombination {
			Combo1 = 1,
			Combo2 = 2,
			Combo3 = 4,
			Combo4 = 8,
			Combo5 = 16,
			Combo6 = 32,
			Combo7 = 64,
			Combo8 = 128,
			Combo9 = 256,
			Combo10 = 512,
			Combo11 = 1024,
			Combo12 = 2048,
			Combo13 = 4096,
			Combo14 = 8192,
			Combo15 = 16384,
			Combo16 = 32768
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdMUHybridUnmanaged {
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridCreate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridCreate();
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridDestroy", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridDestroy(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetPathToMatchUpFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridSetPathToMatchUpFiles(IntPtr i, IntPtr Path);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetMatchcodeName", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridSetMatchcodeName(IntPtr i, IntPtr MatchcodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridSetMatchcodeObject(IntPtr i, IntPtr Matchcode);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridInitializeDataFiles", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridInitializeDataFiles(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetInitializeErrorString", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetInitializeErrorString(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetLicenseString", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridSetLicenseString(IntPtr i, IntPtr License);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetMaximumCharacterSize", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridSetMaximumCharacterSize(IntPtr i, Int32 size);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetEncoding", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridSetEncoding(IntPtr i, IntPtr encodeName);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetBuildNumber", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetBuildNumber(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetDatabaseDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetDatabaseDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetDatabaseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetDatabaseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetLicenseExpirationDate", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetMatchcodeObject", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetMatchcodeObject(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridClearMappings", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridClearMappings(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridAddMapping", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridAddMapping(IntPtr i, Int32 Mapping);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridClearFields", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridClearFields(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridAddField", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridAddField(IntPtr i, IntPtr Field);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridBuildKey", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridBuildKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetKey", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetKey(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetKeySize", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridGetKeySize(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetClusterSize", CallingConvention = CallingConvention.StdCall)]
			public static extern Int32 mdMUHybridGetClusterSize(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridCompareKeys", CallingConvention = CallingConvention.StdCall)]
			public static extern UInt32 mdMUHybridCompareKeys(IntPtr i, IntPtr Key1, IntPtr Key2);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetResults", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetResults(IntPtr i);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridSetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern void mdMUHybridSetReserved(IntPtr i, IntPtr Property, IntPtr Value);
			[DllImport("mdMatchup", EntryPoint = "mdMUHybridGetReserved", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr mdMUHybridGetReserved(IntPtr i, IntPtr Property);
		}

		public mdHybrid() {
			i = mdMUHybridUnmanaged.mdMUHybridCreate();
			EncodedString.SetLegacyUsage(true);
		}

		~mdHybrid() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdMUHybridUnmanaged.mdMUHybridDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToMatchUpFiles(string Path) {
			mdMUHybridUnmanaged.mdMUHybridSetPathToMatchUpFiles(i, (new EncodedString(Path)).GetEncodedPtr());
		}

		public void SetMatchcodeName(string MatchcodeName) {
			mdMUHybridUnmanaged.mdMUHybridSetMatchcodeName(i, (new EncodedString(MatchcodeName)).GetEncodedPtr());
		}

		public void SetMatchcodeObject(IntPtr Matchcode) {
			mdMUHybridUnmanaged.mdMUHybridSetMatchcodeObject(i, Matchcode);
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdMUHybridUnmanaged.mdMUHybridInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetInitializeErrorString(i));
		}

		public int SetLicenseString(string License) {
			return mdMUHybridUnmanaged.mdMUHybridSetLicenseString(i, (new EncodedString(License)).GetEncodedPtr());
		}

		public void SetMaximumCharacterSize(int size) {
			mdMUHybridUnmanaged.mdMUHybridSetMaximumCharacterSize(i, size);
		}

		public int SetEncoding(string encodeName) {
      if (string.Compare(encodeName, "UTF-8", true) == 0)
    	  EncodedString.SetLegacyUsage(false);
			return mdMUHybridUnmanaged.mdMUHybridSetEncoding(i, (new EncodedString(encodeName)).GetEncodedPtr());
		}

		public string GetBuildNumber() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetDatabaseExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetLicenseExpirationDate(i));
		}

		public mdMatchcode GetMatchcodeObject() {
			IntPtr retVal = mdMUHybridUnmanaged.mdMUHybridGetMatchcodeObject(i);
			if (retVal == null)
				return null;
			return new mdMatchcode(retVal);
		}

		public void ClearMappings() {
			mdMUHybridUnmanaged.mdMUHybridClearMappings(i);
		}

		public int AddMapping(MatchcodeMapping Mapping) {
			return mdMUHybridUnmanaged.mdMUHybridAddMapping(i, (int)Mapping);
		}

		public void ClearFields() {
			mdMUHybridUnmanaged.mdMUHybridClearFields(i);
		}

		public void AddField(string Field) {
			if (EncodedString.GetLegacyUsage())
				SetReserved("SkipMBSConvert", "T");
			mdMUHybridUnmanaged.mdMUHybridAddField(i, (new EncodedString(Field)).GetEncodedPtr());
		}

		public void BuildKey() {
			mdMUHybridUnmanaged.mdMUHybridBuildKey(i);
		}

		public string GetKey() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetKey(i));
		}

		public int GetKeySize() {
			return mdMUHybridUnmanaged.mdMUHybridGetKeySize(i);
		}

		public int GetClusterSize() {
			return mdMUHybridUnmanaged.mdMUHybridGetClusterSize(i);
		}

		public uint CompareKeys(string Key1, string Key2) {
			return mdMUHybridUnmanaged.mdMUHybridCompareKeys(i, (new EncodedString(Key1)).GetEncodedPtr(), (new EncodedString(Key2)).GetEncodedPtr());
		}

		public string GetResults() {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetResults(i));
		}

		public void SetReserved(string Property, string Value) {
			mdMUHybridUnmanaged.mdMUHybridSetReserved(i, (new EncodedString(Property)).GetEncodedPtr(), (new EncodedString(Value)).GetEncodedPtr());
		}

		public string GetReserved(string Property) {
			return EncodedString.GetUnicodeString(mdMUHybridUnmanaged.mdMUHybridGetReserved(i, (new EncodedString(Property)).GetEncodedPtr()));
		}

		public bool IsLegacy() {
			return EncodedString.GetLegacyUsage();
		}

		private class EncodedString : IDisposable {
			private IntPtr encodedString = IntPtr.Zero;
			private static bool isLegacy = false;

			public static void SetLegacyUsage(bool legacy) {
				isLegacy = legacy;
			}

			public static bool GetLegacyUsage() {
				return isLegacy;
			}

			public EncodedString(string str) {
				Encoding encoding;
				if (isLegacy)
					encoding = Encoding.GetEncoding("ISO-8859-1");
				else
					encoding = Encoding.UTF8;

				if (str == null)
					str = "";

				byte[] buffer = encoding.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				encodedString = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, encodedString, buffer.Length);
			}

			~EncodedString() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(encodedString);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetEncodedPtr() {
				return encodedString;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);

				if (isLegacy)
					return Encoding.GetEncoding("ISO-8859-1").GetString(buffer);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}