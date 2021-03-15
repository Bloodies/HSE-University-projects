package org.hse.android.database;

import android.content.Context;

import androidx.lifecycle.LiveData;

import java.util.Date;
import java.util.List;

public class HseRepository {
    private DatabaseManager databaseManager;
    private HseDao dao;

    public HseRepository(Context context) {
        databaseManager = DatabaseManager.getInstance(context);
        dao = databaseManager.getHseDao();
    }

    public LiveData<List<GroupEntity>> getGroups() { return dao.getAllGroup(); }

    public LiveData<List<TeacherEntity>> getTeachers() { return dao.getAllTeacher(); }

    public LiveData<List<TimeTableWithTeacherEntity>> getTimeTableTeacherByDate(Date date) {
        return dao.getTimeTableTeacher(date);
    }
}
