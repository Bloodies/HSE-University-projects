package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;
import android.util.Log;
import android.widget.TextView;
import com.google.gson.Gson;
import org.jetbrains.annotations.NotNull;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import okhttp3.ResponseBody;

public class BaseActivity extends AppCompatActivity {

    enum ScheduleType { DAY, WEEK }

    enum ScheduleMode { STUDENT, TEACHER }

    interface OnItemClick { void onClick(ScheduleActivity.ScheduleItem data); }

    private final static String TAG = "BaseActivity";
    public static final String URL = "https://api.ipgeolocation.io/ipgeo?apiKey=b03018f75ed94023a005637878ec0977";

    protected TextView time, current_time;
    protected Date currentTime;
    public static Date time_export;

    private OkHttpClient client = new OkHttpClient();

    protected void getTime(){
        Request request = new Request.Builder().url(URL).build();
        Call call = client.newCall(request);
        call.enqueue(new Callback() {
            @Override
            public void onFailure(@NotNull Call call, @NotNull IOException e) {
                Log.e("tag", e.getMessage());
            }

            @Override
            public void onResponse(@NotNull Call call, @NotNull Response response) {
                parseResponse(response);
            }
        });
    }

    protected void initTime() { getTime(); }

    private void showTime(Date dateTime){
        time = findViewById(R.id.time);
        current_time = findViewById(R.id.current_time);
        if (dateTime == null){ return; }
        currentTime = dateTime;
        SimpleDateFormat simpleDateFormat = null;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.LOLLIPOP) {
            simpleDateFormat = new SimpleDateFormat("HH:mm, E", Locale.forLanguageTag("ru"));
        }
        time_export = currentTime;
        time.setText(String.format("Сейчас: %s", simpleDateFormat.format(currentTime)));
    }

    private void parseResponse(Response response) {
        Gson gson = new Gson();
        ResponseBody body = response.body();
        try {
            if (body == null) { return; }
            String string = body.string();
            Log.d(TAG, string);
            TimeResponse timeResponse = gson.fromJson(string, TimeResponse.class);
            String currentTimeVal = timeResponse.getTimeZone().getCurrentTime();
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS", Locale.getDefault());
            Date dateTime = simpleDateFormat.parse(currentTimeVal);
            // run on UI thread
            runOnUiThread(() -> showTime(dateTime));
        }
        catch (Exception e) { Log.e(TAG, "", e); }
    }
}