namespace MyGame
{
    public static class Initializer
    {
        public static Map CreateMap()
        {
            var map = new Map(
                new Cell[,]
                {
                    {
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Wall,
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Empty,
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty,
                        Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty,
                        Cell.Wall, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty,
                        Cell.Empty, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall,
                        Cell.Wall, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty
                    },
                    {
                        Cell.Monster, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Monster,
                        Cell.Wall, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall,
                        Cell.Wall, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall,
                        Cell.Wall, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty,
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Empty, Cell.Wall,
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Wall, Cell.Monster
                    },
                    {
                        Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty, Cell.Wall,
                        Cell.Wall, Cell.Empty, Cell.Wall, Cell.Wall, Cell.Empty
                    },
                    {
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty,
                        Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty, Cell.Empty
                    }
                }
            );
            return map;
        }
    }
}