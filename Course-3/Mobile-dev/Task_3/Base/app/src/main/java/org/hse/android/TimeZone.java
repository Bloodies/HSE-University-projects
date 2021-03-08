package org.hse.android;

import com.google.gson.annotations.SerializedName;

public class TimeZone {
    @SerializedName("current_time")
    private String currentTime;

    public String getCurrentTime() { return currentTime; }

    public void setCurrentTime(String currentTime) { this.currentTime = currentTime; }
}