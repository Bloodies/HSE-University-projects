package org.hse.android;

import android.content.Context;
import android.content.SharedPreferences;

public class PreferenceManager {
    private final static String PREFERENCE_FILE = "org.hse.android.file";
    private final SharedPreferences sharedPref;

    public PreferenceManager(Context context) {
        sharedPref = context.getSharedPreferences(PREFERENCE_FILE, Context.MODE_PRIVATE);
    }

    ///////////////////////

    public void savePhotoValue(String key, String value) {
        SharedPreferences.Editor editor = sharedPref.edit();
        editor.putString(key, value);
        editor.apply();
    }

    public String getPhotoValue(String key) {
        return sharedPref.getString(key, "");
    }

    private void saveValue(String key, String value) {
        SharedPreferences.Editor editor = sharedPref.edit();
        editor.putString(key, value);
        editor.apply();
    }

    private String getValue(String key, String defaultValue) {
        return sharedPref.getString(key,defaultValue);
    }
}
