package org.hse.android;

import com.google.gson.annotations.SerializedName;

public class TimeResponse {
    @SerializedName("time_zone")
    private TimeZone timeZone;

    public TimeZone getTimeZone() { return timeZone; }

    public void setTimeZone(TimeZone timeZone) { this.timeZone = timeZone; }
}