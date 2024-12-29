namespace AdventOfCode2024.Days;

public class Day09
{
    public static long GetAnswer1()
    {
        var input = GetInput();
        var disk = new Disk(input);

        disk.CompactV1();

        return disk.GetCheckSum();
    }

    public static long GetAnswer2()
    {
        var input = GetInput();
        var disk = new Disk(input);

        disk.CompactV2();

        
        return disk.GetCheckSum();
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

        public void CompactV1()
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

        public void CompactV2()
        {
            var fileIds = this
                .Where(s => s.FileId.HasValue)
                .Select(s => s.FileId!.Value)
                .Distinct()
                .OrderDescending()
                .ToList();

            foreach (var fileId in fileIds)
            {
                var file = this.Where(s => s.FileId == fileId).ToList();
                var sourceStart = file.Min(s => s.Position);
                var fileSize = file.Count;
                var targetStart = GetFreeSpace(fileSize, sourceStart);

                if (targetStart is null) continue;

                for (var i = 0; i < fileSize; i++)
                {
                    this[targetStart.Value+i].Write(this[sourceStart+i].FileId!.Value);
                    this[sourceStart+i].Clear();
                }
            }
        }

        private int? GetFreeSpace(int fileSize, int before)
        {
            var start = 0;

            for (var i = 0; i < before; i++)
            {
                if (!this[i].IsEmpty)
                {
                    start = i + 1;
                    continue;
                }

                var size = i - start + 1;

                if (size == fileSize) return start;
            }

            return null;
        }

        public long GetCheckSum()
        {
            return this.Select((s, i) => (long)i * s.FileId ?? 0).Sum();
        }

        public override string ToString()
        {
            return string.Join("", this.Select(s => s.FileId.HasValue ? s.FileId.Value.ToString().Single() : '.'));
        }

        private static IEnumerable<DiskSegment> Parse(string input)
        {
            var fileId = 0;
            var isFile = true;
            var position = 0;

            for (var i = 0; i < input.Length; i++)
            {
                var value = int.Parse(input[i].ToString());

                if (isFile)
                {
                    for (var j = 0; j < value; j++)
                    {
                        yield return new DiskSegment(position, fileId);
                        position++;
                    }

                    fileId++;
                }
                else
                {
                    for (var j = 0; j < value; j++)
                    {
                        yield return new DiskSegment(position);
                        position++;
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
