using System;
using System.IO;
using System.Collections.Generic;
namespace Libreria
{
    public class WordFrequency
    {
        private string word;
        public string WORD { get { return this.word; } }
        private int frequency;
        public int FREQUENCY
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }
        public WordFrequency(string word)
        {
            this.word = word.Trim().ToLower();
            this.frequency = 1;
        }
        public WordFrequency(string word, int frequency)
        {
            this.word = word.Trim().ToLower();
            this.frequency = frequency;
        }
        public void IncrementFrequency()
        {
            this.frequency++;
        }
        public void FusionFrequency(WordFrequency wf)
        {
            this.frequency += wf.FREQUENCY;
        }
        public void DecrementFrequency()
        {
            this.frequency--;
        }
        public void Substitute(string word)
        {
            this.word = word.Trim().ToLower();
        }
        public void Substitute(string word, int word2Freq)
        {
            this.word = word.Trim().ToLower();
            this.frequency += word2Freq;
        }
        public override string ToString()
        {
            return this.word + "<" + this.frequency + ">";
        }
        public int CompareTo(WordFrequency otro)
        {
            return this.word.CompareTo(otro.word);
        }
        public override bool Equals(object obj)
        {
            return (this.CompareTo((WordFrequency)obj) == 0);
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }
}