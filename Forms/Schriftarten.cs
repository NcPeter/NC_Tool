using System.Collections.Generic;

namespace NC_Tool
{
    static class Schriftarten
    {
        public static int Schriftsatz;

        public static void Font_lesen()
        {
            // ZF = G0 - Fahrt (Z auf Sicherheit / X,Y auf Position)
            // ZC = G1 - Fahrt
            // Fontname: HP1345A
            if (Schriftsatz == 1)
            {
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "0",
             "ZF",
             "X1 Y2",
             "ZC",
             "X11 Y16",
             "ZF",
             "X12 Y12",
             "ZC",
             "X12 Y6",
             "X9 Y0",
             "X3 Y0",
             "X0 Y6",
             "X0 Y12",
             "X3 Y18",
             "X9 Y18",
             "X12 Y12",
             "ZF"
            });     // 0
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "1",
             "ZF",
             "X3 Y0",
             "ZC",
             "X9 Y0",
             "X6 Y0",
             "X6 Y18",
             "X3 Y15",
             "ZF"
            });     // 1
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "2",
             "ZF",
             "X0 Y15",
             "ZC",
             "X3 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y11",
             "X2 Y5",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // 2
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "3",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y7",
             "X9 Y9",
             "X3 Y9",
             "X9 Y9",
             "X12 Y11",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18 ",
             "X0 Y16",
             "ZF"
            });     // 3
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "4",
             "ZF",
             "X9 Y0",
             "ZC",
             "X9 Y18",
             "X0 Y6",
             "X12 Y6",
             "ZF"
            });     // 4
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "5",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X9 Y0",
             "X12 Y2",
             "X12 Y8",
             "X9 Y10",
             "X3 Y10",
             "X0 Y9",
             "X2 Y18",
             "X12 Y18",
             "ZF"
            });     // 5
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "6",
             "ZF",
             "X0 Y7",
             "ZC",
             "X3 Y10",
             "X9 Y10",
             "X12 Y7",
             "X12 Y3",
             "X9 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y10",
             "X3 Y15",
             "X7 Y18",
             "ZF"
            });     // 6
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "7",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y18",
             "X4 Y0",
             "ZF"
            });     // 7
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "8",
             "ZF",
             "X9 Y10",
             "ZC",
             "X12 Y13",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y13",
             "X3 Y10",
             "X9 Y10",
             "X12 Y7",
             "X12 Y3",
             "X9 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y7",
             "X3 Y10",
             "ZF"
            });     // 8
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "9",
             "ZF",
             "X5 Y0",
             "ZC",
             "X9 Y3",
             "X12 Y8",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y11",
             "X3 Y8",
             "X9 Y8",
             "X12 Y11",
             "ZF"
            });     // 9
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ".",
             "ZF",
             "X6 Y0",
             "ZC",
             "ZF"
            });     // .
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "a",
             "ZF",
             "X0 Y10",
             "ZC",
             "X5 Y12",
             "X11 Y10",
             "X11 Y2",
             "X8 Y0",
             "X4 Y0",
             "X0 Y2",
             "X0 Y5",
             "X11 Y6",
             "X11 Y2",
             "X13 Y0",
             "ZF"
            });     // a
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "b",
             "ZF",
             "X0 Y18",
             "ZC",
             "X0 Y0",
             "X0 Y2",
             "X6 Y0",
             "X12 Y2",
             "X12 Y9",
             "X6 Y11",
             "X0 Y9",
             "ZF"
            });     // b
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "c",
             "ZF",
             "X11 Y9",
             "ZC",
             "X6 Y11",
             "X0 Y9",
             "X0 Y2",
             "X6 Y0",
             "X11 Y2",
             "ZF"
            });     // c
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "d",
             "ZF",
             "X12 Y9",
             "ZC",
             "X6 Y11",
             "X0 Y9",
             "X0 Y2",
             "X6 Y0",
             "X12 Y2",
             "X12 Y0",
             "X12 Y18",
             "ZF"
            });     // d
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "e",
             "ZF",
             "X0 Y6",
             "ZC",
             "X12 Y7",
             "X9 Y12",
             "X3 Y12",
             "X0 Y9",
             "X0 Y2",
             "X3 Y0",
             "X9 Y0",
             "X12 Y2",
             "ZF"
            });     // e
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "f",
             "ZF",
             "X0 Y9",
             "ZC",
             "X8 Y9",
             "ZF",
             "X12 Y16",
             "ZC",
             "X8 Y18",
             "X4 Y16",
             "X4 Y0",
             "ZF"
            });     // f
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "g",
             "ZF",
             "X11 Y2",
             "ZC",
             "X6 Y0",
             "X0 Y2",
             "X0 Y9",
             "X6 Y11",
             "X11 Y9",
             "X11 Y11",
             "X11 Y-5",
             "X6 Y-7",
             "X0 Y-5",
             "ZF"
            });     // g
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "h",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X0 Y9",
             "ZC",
             "X6 Y11",
             "X12 Y9",
             "X12 Y0",
             "ZF"
            });     // h
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "i",
             "ZF",
             "X7 Y0",
             "ZC",
             "X7 Y11",
             "X4 Y11",
             "ZF",
             "X7 Y18",
             "ZC",
             "X6 Y18",
             "X6 Y17",
             "X7 Y17",
             "X7 Y18",
             "ZF"
            });     // i
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "j",
             "ZF",
             "X0 Y-5",
             "ZC",
             "X4 Y-7",
             "X8 Y-5",
             "X8 Y11",
             "ZF",
             "X8 Y18",
             "ZC",
             "X7 Y18",
             "X7 Y17",
             "X8 Y17",
             "X8 Y18",
             "ZF"
            });     // j
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "k",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X0 Y5",
             "ZC",
             "X12 Y11",
             "ZF",
             "X4 Y7",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // k
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "l",
             "ZF",
             "X3 Y18",
             "ZC",
             "X6 Y18",
             "X6 Y0",
             "X3 Y0",
             "X9 Y0",
             "ZF"
            });     // l
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "m",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y12",
             "X0 Y9",
             "X4 Y12",
             "X6 Y9",
             "X6 Y0",
             "ZF",
             "X6 Y9",
             "ZC",
             "X10 Y12",
             "X12 Y9",
             "X12 Y0",
             "ZF"
            });     // m
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "n",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y11",
             "X0 Y8",
             "X6 Y11",
             "X12 Y8",
             "X12 Y0",
             "ZF"
            });     // n
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "o",
             "ZF",
             "X6 Y0",
             "ZC",
             "X12 Y2",
             "X12 Y9",
             "X6 Y11",
             "X0 Y9",
             "X0 Y2",
             "X6 Y0",
             "ZF"
            });     // o
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "p",
             "ZF",
             "X0 Y-7",
             "ZC",
             "X0 Y11",
             "X0 Y9",
             "X6 Y11",
             "X12 Y9",
             "X12 Y2",
             "X6 Y0",
             "X0 Y2",
             "ZF"
            });     // p
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "q",
             "ZF",
             "X11 Y2",
             "ZC",
             "X6 Y0",
             "X0 Y2",
             "X0 Y9",
             "X6 Y11",
             "X11 Y9",
             "X11 Y11",
             "X11 Y-6",
             "X13 Y-8",
             "ZF"
            });     // q
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "r",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y11",
             "X0 Y8",
             "X6 Y11",
             "X12 Y8",
             "ZF"
            });     // r
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "s",
             "ZF",
             "X0 Y2",
             "ZC",
             "X6 Y0",
             "X12 Y2",
             "X12 Y5",
             "X0 Y7",
             "X0 Y10",
             "X6 Y12",
             "X12 Y10",
             "ZF"
            });     // s
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "t",
             "ZF",
             "X0 Y11",
             "ZC",
             "X8 Y11",
             "ZF",
             "X4 Y18",
             "ZC",
             "X4 Y2",
             "X8 Y0",
             "X12 Y2",
             "ZF"
            });     // t
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "u",
             "ZF",
             "X0 Y11",
             "ZC",
             "X0 Y2",
             "X6 Y0",
             "X12 Y2",
             "X12 Y11",
             "ZF"
            });     // u
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "v",
             "ZF",
             "X0 Y11",
             "ZC",
             "X6 Y0",
             "X12 Y11",
             "ZF"
            });     // v
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "w",
             "ZF",
             "X0 Y11",
             "ZC",
             "X3 Y0",
             "X6 Y8",
             "X9 Y0",
             "X12 Y11",
             "ZF"
            });     // w
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "x",
             "ZF",
             "X0 Y0",
             "ZC",
             "X11 Y11",
             "ZF",
             "X0 Y11",
             "ZC",
             "X11 Y0",
             "ZF"
            });     // x
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "y",
             "ZF",
             "X0 Y11",
             "ZC",
             "X7 Y1",
             "ZF",
             "X3 Y-7",
             "ZC",
             "X12 Y11",
             "ZF"
            });     // y
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "z",
             "ZF",
             "X0 Y11",
             "ZC",
             "X12 Y11",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // z
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             " ",
             "ZF"
            });     // " "
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "A",
             "ZF",
             "X0 Y0",
             "ZC",
             "X6 Y18",
             "X12 Y0",
             "ZF",
             "X3 Y9",
             "ZC",
             "X9 Y9",
             "ZF"

            });     // A
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "B",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y12",
             "X9 Y9",
             "X0 Y9",
             "X9 Y9",
             "X12 Y6",
             "X12 Y3",
             "X9 Y0",
             "X0 Y0",
             "ZF"
            });     // B
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "C",
             "ZF",
             "X12 Y3",
             "ZC",
             "X9 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y15",
             "ZF"
            });     // C
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "D",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y3",
             "X9 Y0",
             "X0 Y0",
             "ZF"
            });     // D
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "E",
             "ZF",
             "X9 Y9",
             "ZC",
             "X0 Y9",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y18",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // E
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "F",
             "ZF",
             "X9 Y9",
             "ZC",
             "X0 Y9",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y18",
             "X0 Y0",
             "ZF"
            });     // F
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "G",
             "ZF",
             "X12 Y15",
             "ZC",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y8",
             "X5 Y8",
             "ZF"
            });     // G
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "H",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X12 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // H
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "I",
             "ZF",
             "X2 Y0",
             "ZC",
             "X10 Y0",
             "X6 Y0",
             "X6 Y18",
             "X2 Y18",
             "X10 Y18",
             "ZF"
            });     // I
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "J",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X5 Y0",
             "X8 Y2",
             "X8 Y18",
             "X4 Y18",
             "X12 Y18",
             "ZF"
            });     // J
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "K",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y6",
             "X3 Y9",
             "X12 Y0",
             "ZF"
            });     // K
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "L",
             "ZF",
             "X0 Y18",
             "ZC",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // L
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "M",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X6 Y5",
             "X12 Y18",
             "X12 Y0",
             "ZF"
            });     // M
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "N",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X12 Y0",
             "X12 Y18",
             "ZF"
            });     // N
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "O",
             "ZF",
             "X3 Y0",
             "ZC",
             "X9 Y0",
             "X12 Y3",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "ZF"
            });     // O
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "P",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y11",
             "X9 Y8",
             "X0 Y8",
             "ZF"
            });     // P
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Q",
             "ZF",
             "X3 Y0",
             "ZC",
             "X9 Y0",
             "X12 Y3",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "ZF",
             "X7 Y5",
             "ZC",
             "X14 Y-2",
             "ZF"
            });     // Q
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "R",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y11",
             "X9 Y8",
             "X0 Y8",
             "X7 Y8",
             "X12 Y0",
             "ZF"
            });     // R
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "S",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y6",
             "X9 Y9",
             "X3 Y9",
             "X0 Y12",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y16",
             "ZF"
            });     // S
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "T",
             "ZF",
             "X6 Y0",
             "ZC",
             "X6 Y18",
             "X0 Y18",
             "X12 Y18",
             "ZF"
            });     // T
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "U",
             "ZF",
             "X0 Y18",
             "ZC",
             "X0 Y3",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y18",
             "ZF"
            });     // U
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "V",
             "ZF",
             "X0 Y18",
             "ZC",
             "X6 Y0",
             "X12 Y18",
             "ZF"
            });     // V
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "W",
             "ZF",
             "X0 Y18",
             "ZC",
             "X3 Y0",
             "X6 Y14",
             "X9 Y0",
             "X12 Y18",
             "ZF"
            });     // W
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "X",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // X
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Y",
             "ZF",
             "X0 Y18",
             "ZC",
             "X6 Y8",
             "X6 Y0",
             "X6 Y8",
             "X12 Y18",
             "ZF"
            });     // Y
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Z",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y18",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // Z
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "&",
             "ZF",
             "X12 Y5",
             "ZC",
             "X8 Y0",
             "X2 Y0",
             "X0 Y4",
             "X9 Y14",
             "X7 Y18",
             "X3 Y18",
             "X1 Y14",
             "X12 Y0",
             "ZF"
            });     // &
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "'",
             "ZF",
             "X5 Y18",
             "ZC",
             "X5 Y18",
             "X7 Y14",
             "ZF"
            });     // '
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "@",
             "ZF",
             "X12 Y2",
             "ZC",
             "X10 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y6",
             "X5 Y6",
             "X5 Y13",
             "X12 Y13",
             "ZF"
            });     // @
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "\\",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // \\
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "}",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X5 Y1",
             "X5 Y6",
             "X8 Y9",
             "X5 Y12",
             "X5 Y17",
             "X0 Y20",
             "ZF"
            });     // }
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ")",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X6 Y4",
             "X6 Y14",
             "X0 Y20",
             "ZF"
            });     // )
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "]",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X6 Y-2",
             "X6 Y20",
             "X0 Y20",
             "ZF"
            });     // ]
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ":",
             "ZF",
             "X6 Y14",
             "ZC",
             "ZF",
             "X6 Y4",
             "ZC",
             "ZF"
            });     // :
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ",",
             "ZF",
             "X4 Y-4",
             "ZC",
             "X6 Y1",
             "ZF"
            });     // ,
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "$",
             "ZF",
             "X0 Y3",
             "ZC",
             "X3 Y1",
             "X9 Y1",
             "X12 Y3",
             "X12 Y7",
             "X9 Y9",
             "X3 Y9",
             "X0 Y11",
             "X0 Y15",
             "X3 Y17",
             "X9 Y17",
             "X12 Y15",
             "ZF",
             "X6 Y19",
             "ZC",
             "X6 Y-1",
             "ZF"
            });     // $
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "\"",
             "ZF",
             "X3 Y14",
             "ZC",
             "X4 Y18",
             "ZF",
             "X7 Y14",
             "ZC",
             "X8 Y18",
             "ZF"
            });     // \
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "=",
             "ZF",
             "X0 Y4",
             "ZC",
             "X12 Y4",
             "ZF",
             "X0 Y14",
             "ZC",
             "X12 Y14",
             "ZF"
            });     // =
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "!",
             "ZF",
             "X6 Y18",
             "ZC",
             "X6 Y5",
             "ZF",
             "X6 Y0",
             "ZC",
             "ZF"
            });     // !
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "/",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF"
            });     // /
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ">",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y9",
             "X0 Y18",
             "ZF"
            });     // >
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "#",
             "ZF",
             "X2 Y0",
             "ZC",
             "X4 Y18",
             "ZF",
             "X10 Y18",
             "ZC",
             "X8 Y0",
             "ZF",
             "X12 Y5",
             "ZC",
             "X0 Y5",
             "ZF",
             "X0 Y13",
             "ZC",
             "X12 Y13",
             "ZF"
            });     // #
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "<",
             "ZF",
             "X12 Y0",
             "ZC",
             "X0 Y9",
             "X12 Y18",
             "ZF"
            });     // <
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "-",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // -
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "{",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X7 Y1",
             "X7 Y6",
             "X4 Y9",
             "X7 Y12",
             "X7 Y17",
             "X12 Y20",
             "ZF"
            });     // {
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "(",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X6 Y4",
             "X6 Y14",
             "X12 Y20",
             "ZF"
            });     // (
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "[",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X6 Y-2",
             "X6 Y20",
             "X12 Y20",
             "ZF"
            });     // [
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "|",
             "ZF",
             "X6 Y0",
             "ZC",
             "X6 Y6",
             "ZF",
             "X6 Y12",
             "ZC",
             "X6 Y18",
             "ZF"
            });     // |
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "%",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X6 Y14",
             "ZC",
             "X3 Y18",
             "X0 Y14",
             "X3 Y10",
             "X6 Y14",
             "ZF",
             "X9 Y8",
             "ZC",
             "X12 Y4",
             "X9 Y0",
             "X6 Y4",
             "X9 Y8",
             "ZF"
            });     // %
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "+",
             "ZF",
             "X6 Y2",
             "ZC",
             "X6 Y16",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // +
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "?",
             "ZF",
             "X6 Y0",
             "ZC",
             "ZF",
             "X6 Y4",
             "ZC",
             "X6 Y7",
             "X12 Y11",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "ZF"
            });     // ?
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "^",
             "ZF",
             "X0 Y7",
             "ZC",
             "X6 Y16",
             "X12 Y7",
             "ZF"
            });     // ^
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ";",
             "ZF",
             "X5 Y-4",
             "ZC",
             "X7 Y0",
             "ZF",
             "X7 Y10",
             "ZC",
             "ZF"
            });     // ;
            My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "*",
             "ZF",
             "X3 Y2",
             "ZC",
             "X9 Y16",
             "ZF",
             "X3 Y16",
             "ZC",
             "X9 Y2",
             "ZF",
             "X12 Y9",
             "ZC",
             "X0 Y9",
             "ZF"
            });     // *
            }
            // Fontname: Roman S
            if (Schriftsatz == 2)
            {
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "0",
             "ZF",
             "X11,8829 Y3,5273",
             "ZC",
             "X10,0314 Y0,8555",
             "X7,2775 Y0,0",
             "X5,4493",
             "X2,6954 Y0,8555",
             "X0,8556 Y3,5273",
             "X0,000 Y7,7227",
             "Y10,2773",
             "X0,8556 Y14,4727",
             "X2,6954 Y17,1328",
             "X5,4493 Y18,0234",
             "X7,2775",
             "X10,0314 Y17,1328",
             "X11,8829 Y14,4727",
             "X12,7267 Y10,2773",
             "Y7,7227",
             "X11,8829 Y3,5273",
             "ZF",
             "X10,9337 Y3,8789",
             "ZC",
             "X11,7657 Y7,8281",
             "Y10,1602",
             "X10,9337 Y14,1211",
             "X9,422 Y16,3477",
             "X7,1368 Y17,0859",
             "X5,6017",
             "X3,3165 Y16,3477",
             "X1,7931 Y14,1211",
             "X0,9728 Y10,1602",
             "Y7,8281",
             "X1,7931 Y3,8789",
             "X3,3165 Y1,6523",
             "X5,6017 Y0,9141",
             "X7,1368",
             "X9,422 Y1,6523",
             "X10,9337 Y3,8789",
             "ZF"
            });     // 0
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "1",
             "ZF",
             "X8,3411 Y0,0",
             "ZC",
             "X7,3685",
             "Y16,2539",
             "X5,6341 Y14,543",
             "X3,6536 Y13,6055",
             "X3,1497 Y14,4023",
             "X5,0599 Y15,3398",
             "X7,6615 Y17,8594",
             "X8,3411",
             "Y0,0",
             "ZF"
            });     // 1
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "2",
             "ZF",
             "X1,5117 Y13,1953",
             "ZC",
             "Y14,4023",
             "X2,4258 Y16,2188",
             "X3,3867 Y17,1328",
             "X5,25 Y18,0234",
             "X8,8359 Y18,0234",
             "X10,6875 Y17,1328",
             "X11,6484 Y16,2188",
             "X12,5625 Y14,4023",
             "Y12,5508",
             "X11,6602 Y10,8047",
             "X9,9375 Y8,2969",
             "X2,3203 Y0,9141",
             "X13,207",
             "X13,207 Y0,0",
             "X0,0",
             "X9,1875 Y8,8828",
             "X10,8164 Y11,2617",
             "X11,5898 Y12,7852",
             "Y14,1797",
             "X10,8281 Y15,6445",
             "X10,1133 Y16,3477",
             "X8,6016 Y17,0859",
             "X5,4727",
             "X3,9727 Y16,3477",
             "X3,2461 Y15,6328",
             "X2,4727 Y14,1797",
             "Y13,1953",
             "X1,5117",
             "ZF"
            });     // 2
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "3",
             "ZF",
             "X1,9454 Y18,0234",
             "ZC",
             "X12,4454",
             "X7,4063 Y11,5078",
             "X9,0704",
             "X10,9337 Y10,6172",
             "X11,9063 Y9,6445",
             "X12,797 Y7,0312",
             "Y5,2617",
             "X11,9063 Y2,6367",
             "X10,043 Y0,8437",
             "X7,3477 Y0,0",
             "X4,6876",
             "X1,9923 Y0,8437",
             "X0,9962 Y1,793",
             "X0,0 Y3,7266",
             "X0,8555 Y4,1836",
             "X1,8048 Y2,3555",
             "X2,4962 Y1,6641",
             "X4,8282 Y0,9141",
             "X7,2071",
             "X9,5509 Y1,6641",
             "X11,0509 Y3,1406",
             "X11,836 Y5,4023",
             "Y6,8906",
             "X11,0509 Y9,1641",
             "X10,336 Y9,8203",
             "X8,836 Y10,5703",
             "X5,461",
             "X10,5001 Y17,0859",
             "X1,9454",
             "Y18,0234",
             "ZF"
            });     // 3
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "4",
             "ZF",
             "X9,8438 Y6,457",
             "ZC",
             "X13,8516",
             "Y5,5195",
             "X9,8438",
             "Y0,0",
             "X8,8711",
             "Y5,5195",
             "X0,0",
             "X9,1055 Y17,8594",
             "X9,8438",
             "Y6,457",
             "ZF",
             "X8,8711",
             "ZC",
             "Y15,9141",
             "X1,8984 Y6,457",
             "X8,8711",
             "ZF"
            });     // 4
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "5",
             "ZF",
             "X0,8555 Y4,1836",
             "ZC",
             "X1,8047 Y2,3555",
             "X2,4961 Y1,6641",
             "X4,8281 Y0,9141",
             "X7,207",
             "X9,5508 Y1,6641",
             "X11,0508 Y3,1406",
             "X11,8359 Y5,4023",
             "Y6,8906",
             "X11,0508 Y9,1641",
             "X9,5508 Y10,6289",
             "X7,207 Y11,3906",
             "X4,8281",
             "X2,4961 Y10,6289",
             "X0,7734 Y8,918",
             "X1,8047 Y18,0234",
             "X10,9336",
             "Y17,0859",
             "X2,6602",
             "X2,0156 Y11,4844",
             "X1,9922 Y11,4492",
             "X4,6875 Y12,3164",
             "X7,3477",
             "X10,0547 Y11,4492",
             "X11,9063 Y9,6563",
             "X12,7969 Y7,0313",
             "Y5,2617",
             "X11,9063 Y2,6367",
             "X10,043 Y0,8438",
             "X7,3477 Y0,0",
             "X4,6875",
             "X1,9922 Y0,8438",
             "X0,9961 Y1,793",
             "X0,0 Y3,7266",
             "X0,8555 Y4,1836",
             "ZF"
            });     // 5
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "6",
             "ZF",
             "X0,9609 Y9,0352",
             "ZC",
             "X0,8906 Y8,8359",
             "X2,7422 Y10,6289",
             "X5,4492 Y11,5078",
             "X6,4336",
             "X9,1406 Y10,6289",
             "X10,9922 Y8,8359",
             "X11,8828 Y6,2109",
             "Y5,2617",
             "X10,9922 Y2,6367",
             "X9,1289 Y0,8438",
             "X6,4336 Y0,0",
             "X5,4492",
             "X2,7539 Y0,8438",
             "X0,8789 Y2,6484",
             "X0,0 Y6,0938",
             "Y10,2773",
             "X0,8438 Y14,4609",
             "X1,7578 Y16,2188",
             "X2,7539 Y17,1445",
             "X5,4492 Y18,0234",
             "X7,2773",
             "X10,0781 Y17,1328",
             "X11,1211 Y15,0938",
             "X10,2773 Y14,6133",
             "X9,3633 Y16,3477",
             "X7,125 Y17,0859",
             "X5,5898",
             "X3,2578 Y16,3359",
             "X2,5781 Y15,6445",
             "X1,793 Y14,1328",
             "X0,9609 Y10,1602",
             "Y9,0352",
             "ZF",
             "X1,7461 Y8,3438",
             "ZC",
             "X0,9727 Y6,1289",
             "X1,7578 Y3,1289",
             "X3,2578 Y1,6641",
             "X5,5898 Y0,9141",
             "X6,293",
             "X8,625 Y1,6641",
             "X10,1367 Y3,1406",
             "X10,9219 Y5,4023",
             "Y6,082",
             "X10,1367 Y8,3438",
             "X8,625 Y9,8086",
             "X6,293 Y10,5703",
             "X5,5898",
             "X3,2578 Y9,8086",
             "X1,7461 Y8,3438",
             "ZF"
            });     // 6
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "7",
             "ZF",
             "X3,0937 Y0,3281",
             "ZC",
             "X11,2734 Y17,0508",
             "X0,0",
             "Y17,9883",
             "X12,8437",
             "X3,9727 Y0,0",
             "X3,0937 Y0,3281",
             "ZF"
            });     // 7
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "8",
             "ZF",
             "X6,3659 Y10,9102",
             "ZC",
             "X8,7097 Y11,4961",
             "X10,198 Y12,1992",
             "X10,9245 Y13,5938",
             "Y15,0",
             "X10,2097 Y16,3477",
             "X7,9714 Y17,0859",
             "X4,7605",
             "X2,5222 Y16,3477",
             "X1,8073 Y15,0",
             "Y13,5938",
             "X2,5339 Y12,1992",
             "X4,0222 Y11,4961",
             "X6,3659 Y10,9102",
             "ZF",
             "Y9,9492",
             "ZC",
             "X5,6628 Y9,7617",
             "X3,2722 Y9,0",
             "X1,737 Y7,4883",
             "X0,9753 Y6,0352",
             "Y3,8203",
             "X1,737 Y2,3555",
             "X2,4284 Y1,6641",
             "X4,7605 Y0,9141",
             "X7,9714",
             "X10,2565 Y1,6523",
             "X11,7683 Y3,832",
             "Y6,0352",
             "X10,9948 Y7,4883",
             "X9,4714 Y9,0",
             "X7,0808 Y9,7617",
             "X6,3659 Y9,9492",
             "ZF",
             "X4,5144 Y10,3828",
             "ZC",
             "X3,6706 Y10,582",
             "X1,7956 Y11,5078",
             "X0,8464 Y13,3594",
             "Y15,2227",
             "X1,8073 Y17,1328",
             "X4,6198 Y18,0234",
             "X8,1237",
             "X10,9245 Y17,1328",
             "X11,8972 Y15,2227",
             "Y13,3594",
             "X10,9362 Y11,5078",
             "X9,0612 Y10,582",
             "X8,3933 Y10,4297",
             "X9,9753 Y9,8203",
             "X11,8269 Y8,0625",
             "X12,7292 Y6,2578",
             "Y3,5742",
             "X10,8776 Y0,8555",
             "X8,1237 Y0,0",
             "X4,6198",
             "X1,9245 Y0,8438",
             "X0,9284 Y1,793",
             "X0,0026 Y3,5859",
             "Y6,2578",
             "X0,9167 Y8,0625",
             "X2,7565 Y9,8203",
             "X4,5144 Y10,3828",
             "ZF"
            });     // 8
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "9",
             "ZF",
             "X10,9219 Y8,9648",
             "ZC",
             "X10,9922 Y9,1641",
             "X9,1407 Y7,3594",
             "X6,4336 Y6,4922",
             "X5,4493",
             "X2,7422 Y7,3594",
             "X0,8907 Y9,1641",
             "X0,0 Y11,7891",
             "Y12,7383",
             "X0,8907 Y15,3633",
             "X2,754 Y17,1445",
             "X5,4493 Y18,0234",
             "X6,4336",
             "X9,129 Y17,1445",
             "X11,004 Y15,3516",
             "X11,8829 Y11,9063",
             "Y7,7227",
             "X11,0391 Y3,5391",
             "X10,125 Y1,793",
             "X9,129 Y0,8438",
             "X6,4336 Y0,0",
             "X4,6055",
             "X1,8047 Y0,8672",
             "X0,7618 Y2,9063",
             "X1,6055 Y3,3867",
             "X2,5196 Y1,6523",
             "X4,7579 Y0,9141",
             "X6,293",
             "X8,625 Y1,6641",
             "X9,3047 Y2,3555",
             "X10,0899 Y3,8672",
             "X10,9219 Y7,8281",
             "Y8,9648",
             "ZF",
             "X10,1368 Y9,6563",
             "ZC",
             "X10,9102 Y11,8594",
             "X10,125 Y14,8711",
             "X8,625 Y16,3359",
             "X6,293 Y17,0859",
             "X5,5899",
             "X3,2579 Y16,3359",
             "X1,7461 Y14,8594",
             "X0,961 Y12,5977",
             "Y11,918",
             "X1,7461 Y9,6563",
             "X3,2579 Y8,1914",
             "X5,5899 Y7,4297",
             "X6,293",
             "X8,625 Y8,1914",
             "X10,1368 Y9,6563",
             "ZF"
            });     // 9
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ".",
             "ZF",
             "X6,5671 Y0,0",
             "ZC",
             "X5,0437 Y1,4297",
             "X6,5671 Y2,9063",
             "X8,0788 Y1,4297",
             "X6,5671 Y0,0",
             "ZF"
            });     // .
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "a",
             "ZF",
             "X10,0793 Y1,7578",
             "ZC",
             "X9,177 Y0,8672",
             "X7,3254 Y0,0",
             "X4,5715",
             "X2,7082 Y0,8672",
             "X0,9035 Y2,6367",
             "X0,0012 Y5,2617",
             "Y7,0313",
             "X0,9035 Y9,6563",
             "X2,7082 Y11,4375",
             "X4,5715 Y12,3164",
             "X7,3254",
             "X9,177 Y11,4375",
             "X10,0793 Y10,5469",
             "Y12,1406",
             "X11,052",
             "Y0,1641",
             "X10,0793",
             "Y1,7578",
             "ZF",
             "X9,4177 Y2,4465",
             "ZC",
             "X10,0793 Y3,082",
             "Y9,2227",
             "X8,591 Y10,6406",
             "X7,091 Y11,3906",
             "X4,8059",
             "X3,3059 Y10,6406",
             "X1,7473 Y9,1641",
             "X0,9739 Y6,8906",
             "Y5,4023",
             "X1,7473 Y3,1406",
             "X3,3059 Y1,6523",
             "X4,8059 Y0,9141",
             "X7,091",
             "X8,591 Y1,6523",
             "X9,4177 Y2,4465",
             "ZF"
            });     // a
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "b",
             "ZF",
             "X0,0 Y0,1641",
             "ZC",
             "Y17,8477",
             "X0,961",
             "Y10,5469",
             "X1,8633 Y11,4375",
             "X3,7266 Y12,3164",
             "X6,4805",
             "X8,3321 Y11,4375",
             "X10,1485 Y9,6563",
             "X11,0508 Y7,0313",
             "Y5,2617",
             "X10,1485 Y2,6367",
             "X8,3321 Y0,8672",
             "X6,4805 Y0,0",
             "X3,7266",
             "X1,8633 Y0,8672",
             "X0,961 Y1,7578",
             "Y0,1641",
             "X0,0",
             "ZF",
             "X1,9148 Y2,173",
             "ZC",
             "X2,461 Y1,6523",
             "X3,961 Y0,9141",
             "X6,2461",
             "X7,7461 Y1,6523",
             "X9,293 Y3,1406",
             "X10,0782 Y5,4023",
             "Y6,8906",
             "X9,293 Y9,1641",
             "X7,7461 Y10,6406",
             "X6,2461 Y11,3906",
             "X3,961",
             "X2,461 Y10,6406",
             "X0,961 Y9,2227",
             "Y3,082",
             "X1,9148 Y2,173",
             "ZF"
            });     // b
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "c",
             "ZF",
             "X11,1211 Y2,7422",
             "ZC",
             "X9,1758 Y0,8672",
             "X7,3242 Y0,0",
             "X4,5703",
             "X2,707 Y0,8672",
             "X0,9023 Y2,6367",
             "X0,0 Y5,2617",
             "Y7,0313",
             "X0,9023 Y9,6563",
             "X2,707 Y11,4375",
             "X4,5703 Y12,3164",
             "X7,3242",
             "X9,1758 Y11,4375",
             "X11,1328 Y9,5391",
             "X10,418 Y8,8711",
             "X8,5898 Y10,6406",
             "X7,0898 Y11,3906",
             "X4,8047",
             "X3,3047 Y10,6406",
             "X1,7461 Y9,1641",
             "X0,9727 Y6,8906",
             "Y5,4023",
             "X1,7461 Y3,1406",
             "X3,3047 Y1,6523",
             "X4,8047 Y0,9141",
             "X7,0898",
             "X8,5898 Y1,6523",
             "X10,418 Y3,4336",
             "X11,1211 Y2,7422",
             "ZF"
            });     // c
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "d",
             "ZF",
             "X12 Y9",
             "ZC",
             "X6 Y11",
             "X0 Y9",
             "X0 Y2",
             "X6 Y0",
             "X12 Y2",
             "X12 Y0",
             "X12 Y18",
             "ZF"
            });     // d
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "e",
             "ZF",
             "X0 Y6",
             "ZC",
             "X12 Y7",
             "X9 Y12",
             "X3 Y12",
             "X0 Y9",
             "X0 Y2",
             "X3 Y0",
             "X9 Y0",
             "X12 Y2",
             "ZF"
            });     // e
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "f",
             "ZF",
             "X0 Y9",
             "ZC",
             "X8 Y9",
             "ZF",
             "X12 Y16",
             "ZC",
             "X8 Y18",
             "X4 Y16",
             "X4 Y0",
             "ZF"
            });     // f
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "g",
             "ZF",
             "X11 Y2",
             "ZC",
             "X6 Y0",
             "X0 Y2",
             "X0 Y9",
             "X6 Y11",
             "X11 Y9",
             "X11 Y11",
             "X11 Y-5",
             "X6 Y-7",
             "X0 Y-5",
             "ZF"
            });     // g
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "h",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X0 Y9",
             "ZC",
             "X6 Y11",
             "X12 Y9",
             "X12 Y0",
             "ZF"
            });     // h
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "i",
             "ZF",
             "X7 Y0",
             "ZC",
             "X7 Y11",
             "X4 Y11",
             "ZF",
             "X7 Y18",
             "ZC",
             "X6 Y18",
             "X6 Y17",
             "X7 Y17",
             "X7 Y18",
             "ZF"
            });     // i
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "j",
             "ZF",
             "X0 Y-5",
             "ZC",
             "X4 Y-7",
             "X8 Y-5",
             "X8 Y11",
             "ZF",
             "X8 Y18",
             "ZC",
             "X7 Y18",
             "X7 Y17",
             "X8 Y17",
             "X8 Y18",
             "ZF"
            });     // j
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "k",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X0 Y5",
             "ZC",
             "X12 Y11",
             "ZF",
             "X4 Y7",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // k
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "l",
             "ZF",
             "X3 Y18",
             "ZC",
             "X6 Y18",
             "X6 Y0",
             "X3 Y0",
             "X9 Y0",
             "ZF"
            });     // l
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "m",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y12",
             "X0 Y9",
             "X4 Y12",
             "X6 Y9",
             "X6 Y0",
             "ZF",
             "X6 Y9",
             "ZC",
             "X10 Y12",
             "X12 Y9",
             "X12 Y0",
             "ZF"
            });     // m
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "n",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y11",
             "X0 Y8",
             "X6 Y11",
             "X12 Y8",
             "X12 Y0",
             "ZF"
            });     // n
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "o",
             "ZF",
             "X6 Y0",
             "ZC",
             "X12 Y2",
             "X12 Y9",
             "X6 Y11",
             "X0 Y9",
             "X0 Y2",
             "X6 Y0",
             "ZF"
            });     // o
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "p",
             "ZF",
             "X0 Y-7",
             "ZC",
             "X0 Y11",
             "X0 Y9",
             "X6 Y11",
             "X12 Y9",
             "X12 Y2",
             "X6 Y0",
             "X0 Y2",
             "ZF"
            });     // p
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "q",
             "ZF",
             "X11 Y2",
             "ZC",
             "X6 Y0",
             "X0 Y2",
             "X0 Y9",
             "X6 Y11",
             "X11 Y9",
             "X11 Y11",
             "X11 Y-6",
             "X13 Y-8",
             "ZF"
            });     // q
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "r",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y11",
             "X0 Y8",
             "X6 Y11",
             "X12 Y8",
             "ZF"
            });     // r
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "s",
             "ZF",
             "X0 Y2",
             "ZC",
             "X6 Y0",
             "X12 Y2",
             "X12 Y5",
             "X0 Y7",
             "X0 Y10",
             "X6 Y12",
             "X12 Y10",
             "ZF"
            });     // s
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "t",
             "ZF",
             "X0 Y11",
             "ZC",
             "X8 Y11",
             "ZF",
             "X4 Y18",
             "ZC",
             "X4 Y2",
             "X8 Y0",
             "X12 Y2",
             "ZF"
            });     // t
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "u",
             "ZF",
             "X0 Y11",
             "ZC",
             "X0 Y2",
             "X6 Y0",
             "X12 Y2",
             "X12 Y11",
             "ZF"
            });     // u
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "v",
             "ZF",
             "X0 Y11",
             "ZC",
             "X6 Y0",
             "X12 Y11",
             "ZF"
            });     // v
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "w",
             "ZF",
             "X0 Y11",
             "ZC",
             "X3 Y0",
             "X6 Y8",
             "X9 Y0",
             "X12 Y11",
             "ZF"
            });     // w
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "x",
             "ZF",
             "X0 Y0",
             "ZC",
             "X11 Y11",
             "ZF",
             "X0 Y11",
             "ZC",
             "X11 Y0",
             "ZF"
            });     // x
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "y",
             "ZF",
             "X0 Y11",
             "ZC",
             "X7 Y1",
             "ZF",
             "X3 Y-7",
             "ZC",
             "X12 Y11",
             "ZF"
            });     // y
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "z",
             "ZF",
             "X0 Y11",
             "ZC",
             "X12 Y11",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // z
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             " ",
             "ZF"
            });     // " "
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "A",
             "ZF",
             "X0 Y0",
             "ZC",
             "X6 Y18",
             "X12 Y0",
             "ZF",
             "X3 Y9",
             "ZC",
             "X9 Y9",
             "ZF"

            });     // A
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "B",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y12",
             "X9 Y9",
             "X0 Y9",
             "X9 Y9",
             "X12 Y6",
             "X12 Y3",
             "X9 Y0",
             "X0 Y0",
             "ZF"
            });     // B
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "C",
             "ZF",
             "X12 Y3",
             "ZC",
             "X9 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y15",
             "ZF"
            });     // C
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "D",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y3",
             "X9 Y0",
             "X0 Y0",
             "ZF"
            });     // D
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "E",
             "ZF",
             "X9 Y9",
             "ZC",
             "X0 Y9",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y18",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // E
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "F",
             "ZF",
             "X9 Y9",
             "ZC",
             "X0 Y9",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y18",
             "X0 Y0",
             "ZF"
            });     // F
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "G",
             "ZF",
             "X12 Y15",
             "ZC",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y8",
             "X5 Y8",
             "ZF"
            });     // G
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "H",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X12 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // H
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "I",
             "ZF",
             "X2 Y0",
             "ZC",
             "X10 Y0",
             "X6 Y0",
             "X6 Y18",
             "X2 Y18",
             "X10 Y18",
             "ZF"
            });     // I
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "J",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X5 Y0",
             "X8 Y2",
             "X8 Y18",
             "X4 Y18",
             "X12 Y18",
             "ZF"
            });     // J
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "K",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "ZF",
             "X12 Y18",
             "ZC",
             "X0 Y6",
             "X3 Y9",
             "X12 Y0",
             "ZF"
            });     // K
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "L",
             "ZF",
             "X0 Y18",
             "ZC",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // L
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "M",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X6 Y5",
             "X12 Y18",
             "X12 Y0",
             "ZF"
            });     // M
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "N",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X12 Y0",
             "X12 Y18",
             "ZF"
            });     // N
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "O",
             "ZF",
             "X3 Y0",
             "ZC",
             "X9 Y0",
             "X12 Y3",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "ZF"
            });     // O
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "P",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y11",
             "X9 Y8",
             "X0 Y8",
             "ZF"
            });     // P
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Q",
             "ZF",
             "X3 Y0",
             "ZC",
             "X9 Y0",
             "X12 Y3",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "X0 Y3",
             "X3 Y0",
             "ZF",
             "X7 Y5",
             "ZC",
             "X14 Y-2",
             "ZF"
            });     // Q
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "R",
             "ZF",
             "X0 Y0",
             "ZC",
             "X0 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y11",
             "X9 Y8",
             "X0 Y8",
             "X7 Y8",
             "X12 Y0",
             "ZF"
            });     // R
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "S",
             "ZF",
             "X0 Y2",
             "ZC",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y6",
             "X9 Y9",
             "X3 Y9",
             "X0 Y12",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y16",
             "ZF"
            });     // S
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "T",
             "ZF",
             "X6 Y0",
             "ZC",
             "X6 Y18",
             "X0 Y18",
             "X12 Y18",
             "ZF"
            });     // T
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "U",
             "ZF",
             "X0 Y18",
             "ZC",
             "X0 Y3",
             "X3 Y0",
             "X9 Y0",
             "X12 Y3",
             "X12 Y18",
             "ZF"
            });     // U
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "V",
             "ZF",
             "X0 Y18",
             "ZC",
             "X6 Y0",
             "X12 Y18",
             "ZF"
            });     // V
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "W",
             "ZF",
             "X0 Y18",
             "ZC",
             "X3 Y0",
             "X6 Y14",
             "X9 Y0",
             "X12 Y18",
             "ZF"
            });     // W
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "X",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // X
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Y",
             "ZF",
             "X0 Y18",
             "ZC",
             "X6 Y8",
             "X6 Y0",
             "X6 Y8",
             "X12 Y18",
             "ZF"
            });     // Y
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "Z",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y18",
             "X0 Y0",
             "X12 Y0",
             "ZF"
            });     // Z
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "&",
             "ZF",
             "X12 Y5",
             "ZC",
             "X8 Y0",
             "X2 Y0",
             "X0 Y4",
             "X9 Y14",
             "X7 Y18",
             "X3 Y18",
             "X1 Y14",
             "X12 Y0",
             "ZF"
            });     // &
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "'",
             "ZF",
             "X5 Y18",
             "ZC",
             "X5 Y18",
             "X7 Y14",
             "ZF"
            });     // '
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "@",
             "ZF",
             "X12 Y2",
             "ZC",
             "X10 Y0",
             "X3 Y0",
             "X0 Y3",
             "X0 Y15",
             "X3 Y18",
             "X9 Y18",
             "X12 Y15",
             "X12 Y6",
             "X5 Y6",
             "X5 Y13",
             "X12 Y13",
             "ZF"
            });     // @
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "\\",
             "ZF",
             "X0 Y18",
             "ZC",
             "X12 Y0",
             "ZF"
            });     // \\
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "}",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X5 Y1",
             "X5 Y6",
             "X8 Y9",
             "X5 Y12",
             "X5 Y17",
             "X0 Y20",
             "ZF"
            });     // }
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ")",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X6 Y4",
             "X6 Y14",
             "X0 Y20",
             "ZF"
            });     // )
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "]",
             "ZF",
             "X0 Y-2",
             "ZC",
             "X6 Y-2",
             "X6 Y20",
             "X0 Y20",
             "ZF"
            });     // ]
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ":",
             "ZF",
             "X6 Y14",
             "ZC",
             "ZF",
             "X6 Y4",
             "ZC",
             "ZF"
            });     // :
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ",",
             "ZF",
             "X4 Y-4",
             "ZC",
             "X6 Y1",
             "ZF"
            });     // ,
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "$",
             "ZF",
             "X0 Y3",
             "ZC",
             "X3 Y1",
             "X9 Y1",
             "X12 Y3",
             "X12 Y7",
             "X9 Y9",
             "X3 Y9",
             "X0 Y11",
             "X0 Y15",
             "X3 Y17",
             "X9 Y17",
             "X12 Y15",
             "ZF",
             "X6 Y19",
             "ZC",
             "X6 Y-1",
             "ZF"
            });     // $
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "\"",
             "ZF",
             "X3 Y14",
             "ZC",
             "X4 Y18",
             "ZF",
             "X7 Y14",
             "ZC",
             "X8 Y18",
             "ZF"
            });     // \
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "=",
             "ZF",
             "X0 Y4",
             "ZC",
             "X12 Y4",
             "ZF",
             "X0 Y14",
             "ZC",
             "X12 Y14",
             "ZF"
            });     // =
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "!",
             "ZF",
             "X6 Y18",
             "ZC",
             "X6 Y5",
             "ZF",
             "X6 Y0",
             "ZC",
             "ZF"
            });     // !
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "/",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF"
            });     // /
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ">",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y9",
             "X0 Y18",
             "ZF"
            });     // >
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "#",
             "ZF",
             "X2 Y0",
             "ZC",
             "X4 Y18",
             "ZF",
             "X10 Y18",
             "ZC",
             "X8 Y0",
             "ZF",
             "X12 Y5",
             "ZC",
             "X0 Y5",
             "ZF",
             "X0 Y13",
             "ZC",
             "X12 Y13",
             "ZF"
            });     // #
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "<",
             "ZF",
             "X12 Y0",
             "ZC",
             "X0 Y9",
             "X12 Y18",
             "ZF"
            });     // <
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "-",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // -
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "{",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X7 Y1",
             "X7 Y6",
             "X4 Y9",
             "X7 Y12",
             "X7 Y17",
             "X12 Y20",
             "ZF"
            });     // {
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "(",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X6 Y4",
             "X6 Y14",
             "X12 Y20",
             "ZF"
            });     // (
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "[",
             "ZF",
             "X12 Y-2",
             "ZC",
             "X6 Y-2",
             "X6 Y20",
             "X12 Y20",
             "ZF"
            });     // [
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "|",
             "ZF",
             "X6 Y0",
             "ZC",
             "X6 Y6",
             "ZF",
             "X6 Y12",
             "ZC",
             "X6 Y18",
             "ZF"
            });     // |
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "%",
             "ZF",
             "X0 Y0",
             "ZC",
             "X12 Y18",
             "ZF",
             "X6 Y14",
             "ZC",
             "X3 Y18",
             "X0 Y14",
             "X3 Y10",
             "X6 Y14",
             "ZF",
             "X9 Y8",
             "ZC",
             "X12 Y4",
             "X9 Y0",
             "X6 Y4",
             "X9 Y8",
             "ZF"
            });     // %
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "+",
             "ZF",
             "X6 Y2",
             "ZC",
             "X6 Y16",
             "ZF",
             "X0 Y9",
             "ZC",
             "X12 Y9",
             "ZF"
            });     // +
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "?",
             "ZF",
             "X6 Y0",
             "ZC",
             "ZF",
             "X6 Y4",
             "ZC",
             "X6 Y7",
             "X12 Y11",
             "X12 Y15",
             "X9 Y18",
             "X3 Y18",
             "X0 Y15",
             "ZF"
            });     // ?
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "^",
             "ZF",
             "X0 Y7",
             "ZC",
             "X6 Y16",
             "X12 Y7",
             "ZF"
            });     // ^
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             ";",
             "ZF",
             "X5 Y-4",
             "ZC",
             "X7 Y0",
             "ZF",
             "X7 Y10",
             "ZC",
             "ZF"
            });     // ;
                My.MyProject.Forms.Form1.gList.Add(new List<string>
            {
             "*",
             "ZF",
             "X3 Y2",
             "ZC",
             "X9 Y16",
             "ZF",
             "X3 Y16",
             "ZC",
             "X9 Y2",
             "ZF",
             "X12 Y9",
             "ZC",
             "X0 Y9",
             "ZF"
            });     // *
            }
        }
    }
}
