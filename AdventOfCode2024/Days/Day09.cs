namespace AdventOfCode2024.Days;

public class Day09
{
    public static long GetAnswer1()
    {
        var input = GetInput();
        var disk = new Disk(input);

        disk.Compact();

        return disk.GetCheckSum();
    }

    public static int GetAnswer2()
    {
        return 0;
    }

    private static string GetInput()
    {
        return Inputs.ForDay(9)
            .ReadLines()
            .Single();
    }

    private class Disk : List<DiskSegment>
    {
        public Disk(string input) : base(Parse(input)) { }

        public void Compact()
        {
            for (var source = Count - 1; source >= 0; source--)
            {
                if (!this[source].IsEmpty)
                {
                    var target = FindIndex(x => x.IsEmpty);

                    if (target > source) continue;

                    this[target].Write(this[source].FileId!.Value);
                    this[source].Clear();
                }
            }
        }

        public long GetCheckSum()
        {
            return this.Select((s, i) => (long)i * s.FileId ?? 0).Sum();
        }

        //public override string ToString()
        //{
        //    return string.Join("", this.Select(s => s.FileId.HasValue ? s.FileId.Value.ToString().Single() : '.'));
        //}

        private static IEnumerable<DiskSegment> Parse(string input)
        {
            var fileId = 0;
            var isFile = true;

            for (var position = 0; position < input.Length; position++)
            {
                var value = int.Parse(input[position].ToString());

                if (isFile)
                {
                    for (var i = 0; i < value; i++)
                    {
                        yield return new DiskSegment(position, fileId);
                    }

                    fileId++;
                }
                else
                {
                    for (var i = 0; i < value; i++)
                    {
                        yield return new DiskSegment(position);
                    }
                }

                isFile = !isFile;
            }
        }
    }

    private class DiskSegment
    {
        public DiskSegment(int position, int? fileId = null)
        {
            Position = position;
            FileId = fileId;
        }

        public int Position { get; }

        public int? FileId { get; private set; }
        
        public bool IsEmpty => !FileId.HasValue;

        public void Write(int fileId) => FileId = fileId;

        public void Clear() => FileId = null;
    }




}
