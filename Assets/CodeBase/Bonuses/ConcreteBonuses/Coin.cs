namespace BlackBall.Bonuses.ConcreteBonuses
{
    public class Coin : GatheringObject
    {
        public override string GetItemTypeKey => "Coin";
        protected override void HandleBonus()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Money.UpdateMoney(1);
        }
    }
}