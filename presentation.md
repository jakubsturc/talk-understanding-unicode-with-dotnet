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

note: UTF-16 is sometimes referred as unicode

--

### Endianness

> Endianness is the sequential order in which bytes are arranged into larger numerical values when stored in memory or when transmitted over digital links.

note:

* UTF-16 and UTF-32 exists in both formats
* Guliver story

--

### Unicode example

```text
                 Š     t     u     r     c
      UTF-8: C5 A0    74    75    72    63
UTF-16 (LE): 60 01 74 00 75 00 72 00 63 00  
UTF-16 (BE): 01 60 00 74 00 75 00 72 00 63
```

