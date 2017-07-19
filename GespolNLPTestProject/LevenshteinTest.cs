using System;
using System.Collections.Generic;
using System.Linq;
using MinimumEditDistance;
using NUnit.Framework;
using XamarinFormsAssistant.Assistant.Extensions;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistantTest
{
    [TestFixture]
    public class LevenshteinTest
    {
        public List<PreceptoDto> Preceptos = new List<PreceptoDto>();

        [SetUp]
        public void Setup()
        {
            Preceptos = MockPreceptos.PreceptoList;
        }

        [TearDown]
        public void Tear()
        {
            Preceptos = null;
            GC.Collect();
        }

        [Test]
        public void PreceptosLoadOk()
        {
            var target = Preceptos;
            Assert.IsTrue(target != null && target.Any());
        }

        [Test]
        public void LevenstheinDistanceGreaterThanZero()
        {
            var target = Levenshtein.CalculateDistance("CIR 117.2.5B",
                "CIR 117.2.5B CIRCULAR CON UNA PERSONA IGUAL O INFERIOR A 135 CMS, EN EL ASIENTO TRASERO DEL VEHICULO, QUE NO UTILIZA DISPOSITIVO DE RETENCION HOMOLOGADO ADAPTADO A SU TALLA Y PESO CORRECTAMENTE ABROCHADO",
                1);
            Assert.IsTrue(target > 0);
        }

        [Test]
        public void LevenstheinDistanceEqualZero()
        {
            var target = Levenshtein.CalculateDistance("CIR 117.2.5B","CIR 117.2.5B",1);
            Assert.IsTrue(target == 0);
        }

        [Test]
        public void GivenTextReturnIfSatisfyAThreshold()
        {
            var sampleText = "Conduccion temeraria 121";
            var target = LevenshteinWrapper.LevenstheinContains("CIR 3.1.5A CONDUCIR DE FORMA MANIFIESTAMENTE TEMERARIA", sampleText, 0.35f);
            Assert.IsTrue(target > 0.70f);
        }

        [Test]
        public void GivenTextReturnIfSatisfyAThreshold2()
        {
            var sampleText = "Conduccion temeraria 121";
            var target = LevenshteinWrapper.LevenstheinContains("LSV.11.3.1B ZUTILIZAR DURANTE LA CONDUCCION DISPOSITIVOS DE TELEFONIA MOVIL O CUALQUIER OTRO MEDIO O SISTEMA DE COMUNICACION QUE REQUIERA INTERVENCION MANUAL DEL CONDUCTOR", sampleText, 0.35f);
            Assert.IsTrue(target > 0.70f);
        }

        [Test]
        public void GivenTextReturnIfSatisfyAThreshold3()
        {
            var sampleText = "El veloz murciélago hindú comía feliz cardillo y kiwi";
            var target = LevenshteinWrapper.LevenstheinContains("El comia cardo", sampleText, 0.35f);
            Assert.IsTrue(target > 0.70f);
        }

        

        [Test]
        public void GivenTextFindClosestAppearingsOnPreceptos()
        {
            var target = "Conduccion temeraria 121";

            var result = LevenshteinWrapper.GetLevenstheinContainsList(target, Preceptos, 0.35f);

            Assert.IsTrue(result.Any());

        }

        [Test]
        public void GivenTextFindClosestAppearingsOnPreceptos2()
        {
            var target = "12";

            var result = LevenshteinWrapper.GetLevenstheinContainsList(target, Preceptos, 0.35f);

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void GivenTextFindOnePrecepto()
        {
            var target = "CIR 159.1.5A NO RESPETAR LA SEÑAL DE LIMITACION RELATIVA A LA CLASE DE VEHICULO PARA EL CUAL ESTÁ RESERVADO EL ESTACIONAMIENTO EN ESE LUGAR ( S-17)";

            var result = LevenshteinWrapper.GetLevenstheinContainsList(target, Preceptos, 0.35f);

            Assert.AreEqual(1,result.Count);

        }

        [Test]
        public void GivenTextFindClosestAppearingsOnPreceptosZero()
        {
            var target = "asdfghjklñ";

            var result = LevenshteinWrapper.GetLevenstheinContainsList(target, Preceptos, 0.35f);

            Assert.IsTrue(!result.Any());

        }
    }
}