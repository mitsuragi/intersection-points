using Lab1;
namespace UnitTests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void GetBeamFunctionParametrsTest()
        {
            Point2D point1 = new Point2D { X = 0, Y = 2 };
            Point2D point2 = new Point2D { X = 1, Y = 3 };

            double expectedA = 1;
            double expectedB = -1;
            double expectedC = 2;

            double A, C;
            int B;

            Logic.GetBeamFunctionParametrs(point1, point2, out A, out B, out C);

            Assert.AreEqual(expectedA, A);
            Assert.AreEqual(expectedB, B);
            Assert.AreEqual(expectedC, C);
        }

        [TestMethod]
        public void GetIntersectionPointsTest()
        {
            Point2D point1 = new Point2D { X = -3, Y = -3 };
            Point2D point2 = new Point2D { X = 0, Y = 0 };

            Point2D circlePoint = new Point2D { X = 0, Y = 0 };
            double rad = 2;

            Point2D expectedPoint1 = new Point2D { X = -1.414, Y = -1.414 };
            Point2D expectedPoint2 = new Point2D { X = 1.414, Y = 1.414 };

            List<Point2D> points = Logic.GetIntersectionPoints(point1 , point2 , circlePoint , rad);

            Assert.AreEqual(expectedPoint1.ToString(), points[0].ToString());
            Assert.AreEqual(expectedPoint2.ToString(), points[1].ToString());
        }
    }
}