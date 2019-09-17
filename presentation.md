---
theme: "black"
separator: '^\r?\n---\r?\n$'
verticalSeparator: '^\r?\n--\r?\n$'
controls: false
menu: false
history: true
progress: false
slideNumber: false
mouseWheel: true
enableMenu: false
enableChalkboard: false
enableTitleFooter: false
---

# Understanding Unicode with .NET 

_by Jakub Šturc_ 🤓

---

## What is Unicode?

--

> Unicode provides a unique number for every character,<br>
> no matter what the platform,<br>
> no matter what the program,<br>
> no matter what the language.<br>

--

### Why we need this?


```text
          Š  t  u  r  c
  ascii: 53 74 75 72 63
i8859-2: A9 74 75 72 63
win1250: 8A 74 75 72 63
keybcs2: 9B 74 75 72 63
```

--

### Origin story

* 1987 - work start in Xerox & Apple
* 1991 - Unicode Consortium established
* 1996 - Unicode 2.0
* ...
* 2019 - Unicode 12.0

---

## Basic concept

```text
+------------+          +------------+           +----------+
|            |          |            |           |          |
| Bits/bytes +----+---->+ Code Point +-----+---->+  Glyph   |
|            |    |     |            |     |     |          |
+------------+    |     +------------+     |     +----------+
                  |                        |
                  |                        |
                  |                        |
             +----+----+              +----+----+
             |Encoding |              |  Font   |
             +---------+              +---------+

```

note:
* I am not talking about glyphs in this talk
--

## Code point

> Any value in the Unicode codespace; that is, the range of integers from 0 to 10FFFF. Not all code points are assigned to encoded characters.

note:
Q: How many possible code points are there?
A: 17 × 65,536 = 1,114,112

--

## Plane

> A range of 65,536 contiguous Unicode code points, where the first code point is an integer multiple of 65,536.

--

## Block

> A grouping of characters within the Unicode encoding space used for organizing code charts. Each block is a uniquely named, continuous, non-overlapping range of code points, containing a multiple of 16 code points, and starting at a location that is a multiple of 16.

--

## Encodings

* UTF-8 
* UTF-16
* UTF-32

--

### Endianness

> Endianness is the sequential order in which bytes are arranged into larger numerical values when stored in memory or when transmitted over digital links.

note:

* UTF-16 and UTF-32 exists in both formats
* Guliver story

--

```text
                 Š     t     u     r     c
      UTF-8: C5 A0    74    75    72    63
UTF-16 (LE): 60 01 74 00 75 00 72 00 63 00  
UTF-16 (BE): 01 60 00 74 00 75 00 72 00 63
```

--

### BOM

> The byte order mark (BOM) is a Unicode character, U+FEFF BYTE ORDER MARK (BOM), whose appearance as a magic number at the start of a text stream can signal several things to a program reading the text

--

```text
             BOM
      UTF-8: EF BB BF
UTF-16 (LE): FF FE
UTF-16 (BE): FE FF
UTF-32 (LE): FF FE 00 00
UTF-32 (BE): 00 00 FE FF
```

---

## Combining characters

--

### Simple example

```text
            Š           t     u     r     c
UTF-16: 01 60       00 74 00 75 00 72 00 63
            S    _ˇ     t     u     t     c
UTF-16: 00 53 03 0C 00 74 00 75 00 72 00 63
```

--

### Vietnamese example (ế)

```text
            ế
UTF-16: 1E BF
            e    _ˆ    _´
UTF-16: 00 65 03 02 03 01
            e    _´    _ˆ
UTF-16: 00 65 03 01 03 02
            é    _ˆ
UTF-16: 00 E9 03 02
            ê    _´
UTF-16: 00 E9 03 01
```

--

### Normalization

```text
      |
 NFD  | NFC
      |
------+------
      |
 NFKD | NFKC
      |
```

note:

* two dimension decomposed/composed and canonical and compatible
* https://en.wikipedia.org/wiki/Unicode_equivalence

--

### Zalgo

> T͖̦̜o̜͈̩͖̬ ͈̝in̠̫̹̱̼v̳͙̰͖͔̙͖o͇͟k̥̬͎̟̙̞e̤̭̜͎͖̰͇ ͖̦̞̰͔ţ̝̪̹͚̱h͚ͅẹ̵ ̛͕͎̠̯h̳̠̥̰͓͘i̪̗̫̖v̛̰͖̯ͅͅe̷̲̺͕-͕̩̤m̹̗͖͎̮̟̰i̠̯nd͓͘ ̖͉̝̘̠ͅr̤͖̜e͉͞p̹͍rḛ͖s͏͇̼̺̥e̷̥̤ͅṇ̟̻̥̻͞t͓̦i͉̘͚͝ͅn̴̠g͈̫͕ ͇̼c̰̥̺̟͖͠ha̩͡o̧͚͍̞̮s̞̯.̲̗͙̘̠̰̩̀
̲͎̟̞I͓̼̱̞̩͢ͅn͓̘͈̣̟͘v͉̺͙͉̻͉͘ͅo̡̗̺̼̳̰k̩͔͈̝̗̮̹i͇̥͖̩͕n̵̯͎̱g̛̱ ̲̬̦̩̣t̞̗̹̼̼͞h͎͉̳e̖̙ ̪͉̺̪̯f̭̖̳̺eȩ̟͓̝̠̝̣̥l̺i͍͓̲̫̝͖͠n̨̪g̮͖̟̝̺̘͉͞ ̙͇͠o̧̞͔̦͕͇͇̘f̤̙͘ ̠̗̳̥̪͍͎c͠h͇͉̠͙̱̘̳͞a̠̗̞̥̥͜o̵̬͖̤͚̠s̖̬.
Ẁ͖̟i̠̣̳̦͎͙͞ͅt͓ͅh ̦o̥̲͇͚u̠t̨̙̣̜͙ ̳o͍͔r̷͕͇͇̣d̪͇̜͡e҉r͙̀.
̙͍̭T̡̙̤̩̺̥̯̣ẖe̩̻͙̟̹͠ͅ ͢N̡̗̠̯̳̪e̳̫̩͡zp̛̺̺er̴d̡͉̖͇i̡̬͚̥̘͓̦ͅa̺̙̬n̟͈͡ ̧̦̬ẖi̤̱̲̼͉͈͓v͏̲̱̹e̗͇̤̠̟͎-m̼̪͖̦̫̥̯͠i҉͔͎̲̭̙̱͖nd̥̘̀ ̫̟͕͈͍o̞̗͓̺ͅf͇͜ ̴͈̣c̦͚̳͍͉̟͚h̻̰̝̙̯͚a͕̖͚o̧s͓̥͈̝̮̹.͓̣̤͇̲ ̣̲̼͚͙Z͡a̻̹͉͇̦̕l̷̗̺̞̪̺̩g̶̦̥̩̻̫̯͔o̻.͓̻̥
̴͚͚̥̬̤̪H̳̻e̢̯̣͖ ̼̹͚̱̲̭w̵͙̳̣̪̩̩̹ho͍̜͕͎͙͕ ͈͓͉̩W͓̼͈̲a̘͇͈i̮̻̞̻̹̤t̞͈̱̻̟̙̫s̮̻̗̝͍̀ ͓̤B͎͉͡è̙̪̮ͅh̟͖in̙͈͡d̙̭͟ T̪͇̗̰̦h̪̩̬̮̪͈ͅe̗͉̝̙ ̴̤̬̥͓̤W̳̥̥̬͎̰̫a̩̺̕l͇̩͎͝l͞.̹̫͈̖ͅ
̖̫Ẓ̩Ą̗̯͕̖̳͔L̩̻̗G͈͢Ọ͎̟̞͖̩̕!̶


---

## Surrogates

Creating headaches since 1996 (Unicode 2.0)

--

> There are 1024 "high" surrogates (D800–DBFF) and 1024 "low" surrogates (DC00–DFFF).
> In UTF-16, they must always appear in pairs, as a high surrogate followed by a low surrogate, thus using 32 bits to denote one code point. 

--

A surrogate pair denotes the code point 
```
10000₁₆ + (H - D800₁₆) × 400₁₆ + (L - DC00₁₆)
```
where H and L are the numeric values of the high and low surrogates respectively.

--

### 🤔🤯😈

---

## Emoji

note: https://unicode.org/Public/emoji/12.0/

--

### Categories

😃 Smileys & People
🐻 Animals & Nature
🍔 Food & Drink
⚽ Activity
🌇 Travel & Places
💡 Objects
🔣 Symbols
🎌 Flags

--

### Skin tones

* U+1F3FB EMOJI MODIFIER FITZPATRICK TYPE-1-2
* U+1F3FC EMOJI MODIFIER FITZPATRICK TYPE-3
* U+1F3FD EMOJI MODIFIER FITZPATRICK TYPE-4
* U+1F3FE EMOJI MODIFIER FITZPATRICK TYPE-5
* U+1F3FF EMOJI MODIFIER FITZPATRICK TYPE-6

--

### Santa Claus: Medium Skin Tone

🎅 + 🏽 = 🎅🏽

--

### Woman: Medium-Dark Skin Tone, Blond Hair

👱 + 🏾 + U+200D + ♀ + U+FE0F = 👱🏾‍♀️

--

### Family: Man, Woman, Girl

👨 + U+200D + 👩 + U+200D + 👧 = 👨‍👩‍👧

--

### Flags

🇸 + 🇰 = 🇸🇰

🇨 + 🇿 = 🇨🇿

---

## Some amusing scripts

--

### Enclosed Alphanumerics

* ⒶⒷⒸⒹⒺⒻⒼ
* ⓐⓑⓒⓓⓔⓕⓖ
* ①②③④⑤⑥⑦⑧⑨⑩⑪⑫

--

### Alphanumeric Symbols

* 𝐁𝐨𝐥𝐝
* 𝐼𝑡𝑎𝑙𝑖𝑐
* 𝖲𝖺𝗇𝗌-𝖲𝖾𝗋𝗂𝖿
* 𝒮𝒸𝓇𝒾𝓅𝓉
* 𝔉𝔯𝔞𝔨𝔱𝔳𝔯
* 𝙼𝚘𝚗𝚘-𝚂𝚙𝚊𝚌𝚎

--

Unlike previous symbols we can use *uʍop ǝpᴉsdn* to name methods, classes and variables in C#.

---

## In .NET

--

### `String`

> Represents text as a sequence of UTF-16 code units.

--

### `System.Text`

* `Encoding.ASCII`
* `Encoding.UTF8`
* `Encoding.Unicode`
* `Encoding.BigEndianUnicode`
* `Encoding.UTF32`
* `Encoding.GetEncoding("windows-1250")`

--

### Demo - Unpair Surrogate

---

* BOM
* Emoji
* Special Scripts
* Normal forms
* In .NET
 * Setup console
 * Reading/Writing files
 * Everything above

