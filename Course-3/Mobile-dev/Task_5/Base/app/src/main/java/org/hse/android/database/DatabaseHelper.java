package org.hse.android.database;

import androidx.room.Database;
import androidx.room.RoomDatabase;
import androidx.room.TypeConverters;
import org.hse.android.entities.GroupEntity;
import org.hse.android.entities.TeacherEntity;
import org.hse.android.entities.TimeTableEntity;
import org.hse.android.requests.Converters;

@Database(entities = {GroupEntity.class, TeacherEntity.class, TimeTableEntity.class}, version = 1, exportSchema = false)
@TypeConverters({Converters.class})
public abstract class DatabaseHelper extends RoomDatabase {
    public static final String DATABASE_NAME = "hse_time_table";
    public abstract HseDao hseDao();
}