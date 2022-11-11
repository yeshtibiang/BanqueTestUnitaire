using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CompteBancaireNS;

namespace BanqueTests
{
    [TestClass]
    public class CompteBancaireTests
    {
        [TestMethod]
        public void VérifierDébitCompteCorrect()
        {
            // ouvrir un compte
            double soldeInitial = 500000;
            double montantDébit = 400000;
            double soldeAttendu = 100000;
            var compte = new CompteBancaire("Pr. Abdoulaye Diankha", soldeInitial);

            // Débiter
            compte.Débiter(montantDébit);

            // Tester
            double soldeObtenu = compte.Balance;
            Assert.AreEqual(soldeAttendu, soldeObtenu, 0.001, "Compte débité incorrectement");
        }

        [TestMethod]
        public void DébiterMontantNégatifLèveArgumentOutOfRange()
        {
            double soldeInitial = 500000;
            double montantDébit = -10000;

            CompteBancaire compte = new CompteBancaire("Pr. Yeshua Tibiang", soldeInitial);
            try
            {
                compte.Débiter(montantDébit);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, CompteBancaire.DebitMontantInferieurAZeroBalance);
                //StringAssert.Contains(e.Message, "Montant doit être positif");
                return;
            }

            Assert.Fail("L'exception attendu n'est pas lancé");
        }

        [TestMethod]
        public void DébiterMontantSupérieurSoldeLèveArgumentOutOfRange()
        {
            double soldeInitial = 500000;
            double montantDébit = 600000;

            CompteBancaire compte = new CompteBancaire("Pr. Yeshua Tibiang", soldeInitial);
            try
            {
                compte.Débiter(montantDébit);
            }
            catch(System.ArgumentOutOfRangeException e)
            {
                //StringAssert.Contains(e.Message, "Montant débité doit être supérieur ou égal au solde disponible");
                StringAssert.Contains(e.Message, CompteBancaire.DebitMontantSuperieurBalanceMessage);
                return;
            }

            Assert.Fail("L'exception attendu n'est pas lancé");
        }
    }
}
