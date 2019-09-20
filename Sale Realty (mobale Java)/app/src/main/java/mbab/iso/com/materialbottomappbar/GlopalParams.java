package mbab.iso.com.materialbottomappbar;

import android.app.Application;
import android.widget.CheckBox;

import java.util.ArrayList;

public class GlopalParams extends Application {

    private String login ="-1";
    private String id;

    public String getLogin() {
        return login;
    }
    public String getId() { return id; }

    public void setLogin(String login) {
        this.login = login;
    }
    public void setId(String id) { this.id = id; }

    public void setRumCountEdit1(String rumCountEdit1) { this.rumCountEdit1 = rumCountEdit1; }
    public String getRumCountEdit1() { return rumCountEdit1.equals("-1") ? "" : rumCountEdit1; }

    public void setRumCountEdit2(String rumCountEdit2) { this.rumCountEdit2 = rumCountEdit2; }
    public String getRumCountEdit2() { return rumCountEdit2.equals("-1") ? "" : rumCountEdit2; }

    public void setAtajCountEdit1(String atajCountEdit1) { this.atajCountEdit1 = atajCountEdit1; }
    public String getAtajCountEdit1() { return atajCountEdit1.equals("-1") ? "" : atajCountEdit1; }

    public void setAtajCountEdit2(String atajCountEdit2) { this.atajCountEdit2 = atajCountEdit2; }
    public String getAtajCountEdit2() { return atajCountEdit2.equals("-1") ? "" : atajCountEdit2; }

    public void setAtajEdit1(String atajEdit1) { this.atajEdit1 = atajEdit1; }
    public String getAtajEdit1() { return atajEdit1.equals("-1") ? "" : atajEdit1; }

    public void setAtajEdit2(String atajEdit2) { this.atajEdit2 = atajEdit2; }
    public String getAtajEdit2() { return atajEdit2.equals("-1") ? "" : atajEdit2; }

    public void setOuerAreaEdit1(String ouerAreaEdit1) { this.ouerAreaEdit1 = atajEdit2; }
    public String getOuerAreaEdit1() { return ouerAreaEdit1.equals("-1") ? "" : ouerAreaEdit1; }

    public void setOuerAreaEdit2(String ouerAreaEdit2) { this.ouerAreaEdit2 = ouerAreaEdit2; }
    public String getOuerAreaEdit2() { return ouerAreaEdit2.equals("-1") ? "" : ouerAreaEdit2; }

    public void setLiveAreaEdit1(String liveAreaEdit1) { this.liveAreaEdit1 = liveAreaEdit1; }
    public String getLiveAreaEdit1() { return liveAreaEdit1.equals("-1") ? "" : liveAreaEdit1; }

    public void setLiveAreaEdit2(String liveAreaEdit2) { this.liveAreaEdit2 = liveAreaEdit2; }
    public String getLiveAreaEdit2() { return liveAreaEdit2.equals("-1") ? "" : liveAreaEdit2; }

    public void setKykAreaEdit1(String kykAreaEdit1) { this.kykAreaEdit1 = kykAreaEdit1; }
    public String getKykAreaEdit1() { return kykAreaEdit1.equals("-1") ? "" : kykAreaEdit1; }

    public void setKykAreaEdit2(String kykAreaEdit2) { this.kykAreaEdit2 = kykAreaEdit2; }
    public String getKykAreaEdit2() { return kykAreaEdit2.equals("-1") ? "" : kykAreaEdit2; }

    public void setPriceEdit1(String priceEdit1) { this.priceEdit1 = priceEdit1; }
    public String getPriceEdit1() { return priceEdit1.equals("-1") ? "" : priceEdit1; }

    public void setPriceEdit2(String priceEdit2) { this.priceEdit2 = priceEdit2; }
    public String getPriceEdit2() { return priceEdit2.equals("-1") ? "" : priceEdit2; }


    String rumCountEdit1="-1";
    String rumCountEdit2="-1";

    String atajCountEdit1="-1";
    String atajCountEdit2="-1";

    String atajEdit1="-1";
    String atajEdit2="-1";

    String ouerAreaEdit1="-1";
    String ouerAreaEdit2="-1";

    String liveAreaEdit1="-1";
    String liveAreaEdit2="-1";

    String kykAreaEdit1="-1";
    String kykAreaEdit2="-1";

    String priceEdit1="-1";
    String priceEdit2="-1";


    String mebCheck = "0";
    String kykMebCheck = "0";
    String frizCheck = "0";
    String balconCheck= "0";
    String inetCheck  = "0";
    String kondicCheck = "0";
    String woshMashinCheck= "0";

    ArrayList<String> hels;

    public void setMebCheck(String mebCheck) { this.mebCheck = mebCheck; }
    public String getMebCheck() { return mebCheck; }

    public void setKykMebCheck(String kykMebCheck) { this.kykMebCheck = kykMebCheck; }
    public String getKykMebCheck() { return kykMebCheck; }

    public void setFrizCheck(String frizCheck) { this.frizCheck = frizCheck; }
    public String getFrizCheck() { return frizCheck; }

    public void setBalconCheck(String balconCheck) { this.balconCheck = balconCheck; }
    public String getBalconCheck() { return balconCheck; }

    public void setInetCheck(String inetCheck) { this.inetCheck = inetCheck; }
    public String getInetCheck() { return inetCheck; }

    public void setKondicCheck(String kondicCheck) { this.kondicCheck = kondicCheck; }
    public String getKondicCheck() { return kondicCheck; }

    public void setWoshMashinCheck(String woshMashinCheck) { this.woshMashinCheck = woshMashinCheck; }
    public String getWoshMashinCheck() { return woshMashinCheck; }

    public void setHels(ArrayList<String> hels) { this.hels = hels; }
    public ArrayList<String> getHels() { return hels; }

}
