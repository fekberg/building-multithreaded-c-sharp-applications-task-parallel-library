﻿using System;

namespace StockAnalyzer.Core.Domain
{
    public class StockPrice
    {
        public string Identifier  { get; set; }
        public DateTime TradeDate  { get; set; }
        public decimal? Open  { get; set; }
        public decimal? High  { get; set; }
        public decimal? Low  { get; set; }
        public decimal? Close  { get; set; }
        public int Volume  { get; set; }
        public decimal Change  { get; set; }
        public decimal ChangePercent { get; set; }
    }

    public class StockCalculation
    {
        public string Identifier { get; set; }
        public decimal? Result { get; set; }
        public int TotalSeconds { get; set; }
    }
}