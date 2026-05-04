using Players;
using UnityEngine;

public interface IQteListen
{
    public void OnQteStart();
    public void OnQteSucces(GlobalPlayer globalPlayer);
    public void OnQteFail();
    public int QteRound();
}
