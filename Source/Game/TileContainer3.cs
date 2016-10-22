namespace NoughtsAndCrosses
{
    public class TileContainer3
    {
        Object[] elements = new Object[3];

        public TileContainer3(Object elementOne, Object elementTwo, Object elementThree)
        {
            elements[0] = elementOne;
            elements[1] = elementTwo;
            elements[2] = elementThree;
        }

        public ObjectType AllOfSameType()
        {
            if (elements[0] == null || elements[1] == null || elements[2] == null)
                return ObjectType.NA;

            bool oneAndTwo = elements[0].Type == elements[1].Type;
            bool oneAndThree = elements[0].Type == elements[2].Type;
            bool twoAndThree = elements[1].Type == elements[2].Type;

            if (oneAndTwo && twoAndThree && oneAndThree)
                return elements[0].Type;
            else
                return ObjectType.NA;
        }
    }
}
