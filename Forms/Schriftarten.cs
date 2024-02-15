using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace NC_Tool
{
    static class Schriftarten
    {
        public static int Schriftsatz;

        public static void Font_lesen()
        {
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
            }       // Font name: HP1345A
            if (Schriftsatz == 2)
            {

            }       //
        }
    }
}
