using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public record Slice(int StartIndex, int Width)
{
    public Slice(int width) : this(0, width) { }
    
    public int EndIndex => this.StartIndex + this.Width;

    public bool InRange(int maxIndex) =>
       StartIndex >= 0 && EndIndex < maxIndex;

    public Slice Reset() =>
        this with { StartIndex = -1 };

    public static Slice operator ++(Slice slice) => 
        slice with { StartIndex = slice.StartIndex + 1 };

    public static Slice operator --(Slice slice) => 
        slice with { StartIndex = slice.StartIndex - 1 };

    public static Slice operator +(Slice slice, int value) => 
        slice with { StartIndex = slice.StartIndex + value };

    public static Slice operator -(Slice slice, int value) => 
        slice with { StartIndex = slice.StartIndex - value };
}
