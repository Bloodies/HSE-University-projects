package org.hse.android.database;

import android.content.Context;
import androidx.annotation.NonNull;
import androidx.room.Room;
import androidx.room.RoomDatabase;
import androidx.sqlite.db.SupportSQLiteDatabase;
import org.hse.android.entities.GroupEntity;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableEntity;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.concurrent.Executors;

public  class DatabaseManager {
    private  DatabaseHelper db;

    private static DatabaseManager instance;

    public static DatabaseManager getInstance(Context context) {
        if (instance == null) instance = new DatabaseManager(context.getApplicationContext());
        return instance;
    }

    private DatabaseManager(Context context) {
        db = Room.databaseBuilder(context, DatabaseHelper.class, DatabaseHelper.DATABASE_NAME)
                .addCallback(new RoomDatabase.Callback() {
                    @Override public void onCreate(@NonNull SupportSQLiteDatabase db) {
                        Executors.newSingleThreadScheduledExecutor().execute(() -> initData(context));
                    }}).build();
    }

    public HseDao getHseDao() { return db.hseDao(); }

    private void initData(Context context) {
        List<GroupEntity> groups = new ArrayList<>();
        GroupEntity group = new GroupEntity();
        group.id = 1;
        group.name = "ПИ-18-1";
        groups.add(group);
        group = new GroupEntity();
        group.id = 2;
        group.name = "ПИ-18-2";
        groups.add(group);
        DatabaseManager.getInstance(context).getHseDao().insertGroup(groups);

        List<TeacherEntity> teachers = new ArrayList<>();
        TeacherEntity teacher = new TeacherEntity();
        teacher.id = 1;
        teacher.fio = "Петров Пётр Петрович";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 2;
        teacher.fio = "Андреев Андрей Андреевич";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 3;
        teacher.fio = "Дмитриев Дмитрий Дмитриевич";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 4;
        teacher.fio = "Кычкин Алексей Владимирович";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 5;
        teacher.fio = "Бартов Олег Борисович";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 6;
        teacher.fio = "Куприн Валентин Павлович";
        teachers.add(teacher);
        teacher = new TeacherEntity();
        teacher.id = 7;
        teacher.fio = "Карзенкова Александра Владимировна";
        teachers.add(teacher);
        DatabaseManager.getInstance(context).getHseDao().insertTeacher(teachers);

        List<TimeTableEntity> timeTables = new ArrayList<>();
        TimeTableEntity timeTable = new TimeTableEntity();
        timeTable.id = 1;
        timeTable.cabinet = "Кабинет 1";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Философия";
        timeTable.corp = "К1";
        timeTable.type = "ЛЕКЦИЯ";
        timeTable.timeStart = dateFromString("2021-02-04 10:00");
        timeTable.timeEnd = dateFromString("2021-02-04 11:30");
        timeTable.groupId = 1;
        timeTable.teacherId = 1;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 2;
        timeTable.cabinet = "Кабинет 2";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Мобильная разработка";
        timeTable.corp = "К1";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-02-04 13:00");
        timeTable.timeEnd = dateFromString("2021-02-04 15:00");
        timeTable.groupId = 1;
        timeTable.teacherId = 2;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 3;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К1";
        timeTable.type = "ЛЕКЦИЯ";
        timeTable.timeStart = dateFromString("2021-04-05 08:10");
        timeTable.timeEnd = dateFromString("2021-04-05 09:30");
        timeTable.groupId = 1;
        timeTable.teacherId = 4;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 4;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К1";
        timeTable.type = "ЛЕКЦИЯ";
        timeTable.timeStart = dateFromString("2021-04-05 08:10");
        timeTable.timeEnd = dateFromString("2021-04-05 09:30");
        timeTable.groupId = 2;
        timeTable.teacherId = 4;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 5;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Экономика программной инженерии";
        timeTable.corp = "К1";
        timeTable.type = "ЛЕКЦИЯ";
        timeTable.timeStart = dateFromString("2021-04-05 09:40");
        timeTable.timeEnd = dateFromString("2021-04-05 12:50");
        timeTable.groupId = 1;
        timeTable.teacherId = 5;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 6;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Экономика программной инженерии";
        timeTable.corp = "К1";
        timeTable.type = "ЛЕКЦИЯ";
        timeTable.timeStart = dateFromString("2021-04-05 09:40");
        timeTable.timeEnd = dateFromString("2021-04-05 12:50");
        timeTable.groupId = 2;
        timeTable.teacherId = 5;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 7;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-06 08:10");
        timeTable.timeEnd = dateFromString("2021-04-06 09:30");
        timeTable.groupId = 2;
        timeTable.teacherId = 6;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 8;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-07 08:10");
        timeTable.timeEnd = dateFromString("2021-04-07 09:30");
        timeTable.groupId = 2;
        timeTable.teacherId = 6;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 9;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-07 08:10");
        timeTable.timeEnd = dateFromString("2021-04-07 09:30");
        timeTable.groupId = 1;
        timeTable.teacherId = 6;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 10;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Проектирование архитектуры программ.систем";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-09 08:10");
        timeTable.timeEnd = dateFromString("2021-04-09 09:30");
        timeTable.groupId = 1;
        timeTable.teacherId = 6;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 11;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Интеллектуальное право";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-09 09:40");
        timeTable.timeEnd = dateFromString("2021-04-09 11:00");
        timeTable.groupId = 1;
        timeTable.teacherId = 7;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 12;
        timeTable.cabinet = "Дистанционно";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Интеллектуальное право";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-09 11:30");
        timeTable.timeEnd = dateFromString("2021-04-09 12:50");
        timeTable.groupId = 2;
        timeTable.teacherId = 7;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 13;
        timeTable.cabinet = "110";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Экономика программной инженерии";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-10 08:10");
        timeTable.timeEnd = dateFromString("2021-04-10 11:00");
        timeTable.groupId = 1;
        timeTable.teacherId = 5;
        timeTables.add(timeTable);
        timeTable = new TimeTableEntity();
        timeTable.id = 14;
        timeTable.cabinet = "110";
        timeTable.subGroup = "ПИ";
        timeTable.subjName = "Экономика программной инженерии";
        timeTable.corp = "К3";
        timeTable.type = "ПРАКТИЧЕСКОЕ ЗАНЯТИЕ";
        timeTable.timeStart = dateFromString("2021-04-10 11:30");
        timeTable.timeEnd = dateFromString("2021-04-10 14:30");
        timeTable.groupId = 2;
        timeTable.teacherId = 5;
        timeTables.add(timeTable);
        DatabaseManager.getInstance(context).getHseDao().insertTimeTable(timeTables);
    }

    private Date dateFromString(String value) {
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.getDefault());
        try { return simpleDateFormat.parse(value); }
        catch (ParseException ignored) { }
        return null;
    }
}