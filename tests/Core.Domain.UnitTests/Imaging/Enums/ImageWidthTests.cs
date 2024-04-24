using Core.Domain.Imaging.Enums;

namespace Core.Domain.UnitTests.Imaging.Enums;

[TestClass]
public class ImageWidthTests
{
    [DataTestMethod]
    [DataRow(ImageWidth.Large, 1024)]
    [DataRow(ImageWidth.Medium, 300)]
    [DataRow(ImageWidth.Thumbnail, 150)]
    public void ToInt_MapsToDefinedSizes(ImageWidth imageWidth, int expected)
    {
        // Arrange

        // Act
        var actual = imageWidth.ToInt();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
