package org.hse.android.database;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(entities = {GroupEntity.class, TeacherEntity.class, TimeTableEntity.class},
        version = 1,
        exportSchema = false)
public abstract class DatabaseHelper extends RoomDatabase {
    public static final String DATABASE_NAME = "hse_time_table";
    public abstract HseDao hseDao();
}

