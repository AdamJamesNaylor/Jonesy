
namespace AJN.Jonesy.Business.UnitTests {
    using System;
    using System.IO;
    using System.Xml.Linq;
    using Model;
    using Services;
    using Xunit;

    public class QuestionXmlParserTests {
        private readonly string _xml = @"
<questions>
  <question id=""123"">
    <text>How many items can be stored in the inventory?</text>
    <answer>
      <text><![CDATA[<p>37 stacks &amp; 4 pieces of armour.</p>]]></text>
      <details>
        <![CDATA[<p>27 stacks can be stored in the main inventory, 9 in the hotbar, 1 off-hand slot and 4 armour slots.Giving a potential maximum of 2372 individual items.</p>]]>
      </details>
    </answer>
    <audit>
      <created datetime=""2016-07-05T17:30"" by=""0""></created>
      <modified datetime=""2016-07-05T17:30"" by=""0""></modified>
      <verified datetime=""2016-07-05T17:30"" by=""0"">
        <version edition=""Computer"" releaseNumber=""1.10"" />
      </verified>
    </audit>
  </question>
</questions>";

        private XElement _doc;

        public QuestionXmlParserTests() {
            var stream = new StringReader(_xml);

            _doc = XElement.Load(stream);
        }

        [Fact]
        public void Parse_WithCreatedAudit_ReturnsQuestionWithCreatedAudit() {
            var sut = new QuestionXmlParser();

            var result = sut.Parse(_doc.Element("question"));

            Assert.NotNull(result.Audit);
            Assert.NotNull(result.Audit.Created);

            AssertDate(result.Audit.Created.On);

            Assert.Equal(0, result.Audit.Created.By);
        }

        [Fact]
        public void Parse_WithModifiedAudit_ReturnsQuestionWithModifiedAudit()
        {
            var sut = new QuestionXmlParser();

            var result = sut.Parse(_doc.Element("question"));

            Assert.NotNull(result.Audit);
            Assert.NotNull(result.Audit.Modified);

            AssertDate(result.Audit.Modified.On);

            Assert.Equal(0, result.Audit.Modified.By);
        }

        [Fact]
        public void Parse_WithVerifiedAudit_ReturnsQuestionWithVerifiedAudit() {
            var sut = new QuestionXmlParser();

            var result = sut.Parse(_doc.Element("question"));

            Assert.NotNull(result.Audit);
            Assert.NotNull(result.Audit.Verified);

            AssertDate(result.Audit.Verified.On);

            Assert.Equal(0, result.Audit.Verified.By);

            Assert.NotNull(result.Audit.Verified.Version);
            Assert.Equal(GameEdition.Computer.Name, result.Audit.Verified.Version.Edition.Name);
            Assert.Equal("1.10", result.Audit.Verified.Version.ReleaseNumber);

        }

        private void AssertDate(DateTime createdDate) {
            Assert.Equal(2016, createdDate.Year);
            Assert.Equal(7, createdDate.Month);
            Assert.Equal(5, createdDate.Day);
            Assert.Equal(17, createdDate.Hour);
            Assert.Equal(30, createdDate.Minute);
        }

    }
}