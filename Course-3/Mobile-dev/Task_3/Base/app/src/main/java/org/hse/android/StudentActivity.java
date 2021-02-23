package org.hse.android;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import java.text.DateFormatSymbols;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Objects;

public class StudentActivity extends AppCompatActivity {

    private static final String TAG = "StudentActivity";

    private TextView time, status, subject, cabinet, corp, teacher;
    public Date currentTime;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student);
        Objects.requireNonNull(getSupportActionBar()).hide();

        final Spinner spinner = findViewById(R.id.groupList);

        List<Group> groups = new ArrayList<>();
        initGroupList(groups);

        ArrayAdapter<?> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, groups);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        spinner.setAdapter(adapter);

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> parent, View itemSelected, int selectedItemPosition, long selectedId) {
                Object item = adapter.getItem(selectedItemPosition);
                Log.d(TAG,"selectedItem: " + item);
            }
            public void onNothingSelected(AdapterView<?> parent) { }
        });

        time = findViewById(R.id.time);
        initTime();

        status = findViewById(R.id.status);
        subject = findViewById(R.id.subject);
        cabinet = findViewById(R.id.cabinet);
        corp = findViewById(R.id.corp);
        teacher = findViewById(R.id.teacher);

        initData();
    }

    private void initGroupList(List<Group> groups){
        String[] pr = { "ПИ", "БИ", "УБ", "Э", "И", "Ю" };
        String[] yr = { "16", "17", "18", "19", "20" };
        int i=0;
        for (String p : pr) {
            for (String y : yr) {
                for (int z = 1; z < 5; z++) {
                    i++;
                    groups.add(new Group(i, p + "-" + y + "-" + z));
                }
            }
        }
    }

    private void initTime(){
        currentTime = new Date();
        String[] Week_days = { "", "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Субота" };
        DateFormatSymbols symbols = new DateFormatSymbols( new Locale("en", "US"));
        symbols.setShortWeekdays(Week_days);

        @SuppressLint("SimpleDateFormat") SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm',' E", symbols);
        time.setText(simpleDateFormat.format(currentTime));
    }

    private void initData(){
        status.setText("Нет пар");

        subject.setText("Дисциплина");
        cabinet.setText("Кабинет");
        corp.setText("Корпус");
        teacher.setText("Преподователь");
    }

    static class Group{
        private Integer id;
        private String name;

        public Group(Integer id, String name){
            this.id = id;
            this.name = name;
        }
        public Integer getId(){ return id; }
        public void setId(Integer id) { this.id = id; }
        @Override public String toString() { return name; }
        public String getName() { return name; }
        public void setName(String name){ this.name = name; }
    }
}