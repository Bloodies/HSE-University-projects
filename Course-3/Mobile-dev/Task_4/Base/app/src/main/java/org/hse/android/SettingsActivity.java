package org.hse.android;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import org.jetbrains.annotations.NotNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.FileProvider;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Objects;
import com.bumptech.glide.Glide;

public class SettingsActivity extends AppCompatActivity implements SensorEventListener {

    private final static String TAG = "SettingsActivity";

    private static final int REQUEST_IMAGE_CAPTURE = 10;
    private static final int REQUEST_PERMISSION_CODE = 1;

    private final static String PERMISSION = Manifest.permission.CAMERA;

    private ImageView photo;
    private EditText name;

    private PreferenceManager preferenceManager;
    private SensorManager sensorManager;
    private Sensor light;
    private TextView sensorLight;

    File imagePath;
    File image;
    Boolean photo_changed = false;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        Objects.requireNonNull(getSupportActionBar()).hide();

        preferenceManager = new PreferenceManager(this);
        ListView sensors_list = findViewById(R.id.sensors_list);
        name = findViewById(R.id.name);
        photo = findViewById(R.id.photo);
        sensorLight = findViewById(R.id.sensor_light);
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        light = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);

        View buttonPhoto = findViewById(R.id.button_photo);
        buttonPhoto.setOnClickListener(v -> checkPermission());

        View buttonSave = findViewById(R.id.button_save);
        buttonSave.setOnClickListener(v -> {
            loadPhoto();
            preferenceManager.saveValue("name", name.getText().toString());
            if(photo_changed) {
                preferenceManager.saveValue("photo", image.getPath());
            }
            Toast.makeText(SettingsActivity.this, "Данные сохранены", Toast.LENGTH_SHORT).show();
        });

        name.setText(preferenceManager.getValue("name"));
        File imgFile = new File(preferenceManager.getValue("photo"));
        if(imgFile.exists()){ Glide.with(this).load(imgFile).into(photo); }
        else{ photo.setImageResource(R.drawable.no_image); }
        loadPhoto();

        if(sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT) == null) { sensorLight.setText("Нет датчика освещенности"); }

        List<Sensor> listSensor = sensorManager.getSensorList(Sensor.TYPE_ALL);
        List<String> listSensorType = new ArrayList<>();
        for (int i = 0; i < listSensor.size(); i++) { listSensorType.add(listSensor.get(i).getName()); }

        ArrayAdapter<?> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, listSensorType);
        sensors_list.setAdapter(adapter);
    }

    @Override
    protected void onResume() {
        super.onResume();

        name.setText(preferenceManager.getValue("name"));

        File imgFile = new File(preferenceManager.getValue("photo"));
        if (imgFile.exists()) { Glide.with(this).load(imgFile).into(photo); }
        else { photo.setImageResource(R.drawable.no_image); }

        sensorManager.registerListener(this, light, SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onPause() {
        super.onPause();

        name.setText(preferenceManager.getValue("name"));

        File imgFile = new File(preferenceManager.getValue("photo"));
        if(imgFile.exists()){ Glide.with(this).load(imgFile).into(photo); }
        else{ photo.setImageResource(R.drawable.no_image); }

        sensorManager.unregisterListener(this);
    }

    public final void onAccuracyChanged(Sensor sensor, int accuracy) { }

    public final void onSensorChanged(SensorEvent event) {
        float lux = event.values[0];
        sensorLight.setText(String.format("%s lux", lux));
    }

    public void checkPermission(){
        int permissionsCheck = ActivityCompat.checkSelfPermission(this, PERMISSION);
        if (permissionsCheck != PackageManager.PERMISSION_GRANTED) {
            if(ActivityCompat.shouldShowRequestPermissionRationale(this, PERMISSION)) {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.setTitle("Нужно предоставить права")
                        .setMessage("Для снятия фото нужно предоставить права на фото")
                        .setPositiveButton(android.R.string.ok, (dialog, id) ->
                                requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE));
                builder.create().show();
            }
            else { requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE); }
        }
        else { dispatchTakePictureIntent(); }
    }
    
    private void requestPermissions(String permissionName, int permissionRequestCode) {
        ActivityCompat.requestPermissions(this, new String[]{permissionName}, permissionRequestCode);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NotNull String[] permissions, @NotNull int[] result) {
        if(requestCode == REQUEST_PERMISSION_CODE) {
            if(result.length > 0 && result[0] == PackageManager.PERMISSION_GRANTED) {
                dispatchTakePictureIntent();
            }
            else {
                Log.d(TAG, "Permission not granted");
                requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE);
            }
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode == RESULT_OK){
            loadPhoto();
            return;
        }
        super.onActivityResult(requestCode, resultCode, data);
    }

    private void loadPhoto() { if(image != null) Glide.with(this).load(image).into(photo); }

    @SuppressLint("QueryPermissionsNeeded")
    private void dispatchTakePictureIntent() {
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        if(takePictureIntent.resolveActivity(getPackageManager()) != null) {
            //Create a file to store the image
            File photoFile = null;
            try { photoFile = createImageFile(); }
            catch (IOException ex) { Log.e(TAG, "Create file", ex); }
            if(photoFile != null) {
                Uri photoURI = FileProvider.getUriForFile(this, BuildConfig.APPLICATION_ID + ".provider", photoFile);
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI);
                try { startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE); }
                catch (ActivityNotFoundException e) { Log.e(TAG, "Start activity", e); }
            }
        }
        loadPhoto();
    }

    private File createImageFile() throws IOException {
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.getDefault()).format(new Date());
        String imageFileName = "IMG_" + timeStamp + "_";

        imagePath = new File(getFilesDir(), "external_files");
        imagePath.mkdir();
        image = new File(imagePath.getPath(), String.format("%s.img", imageFileName));
        photo_changed = true;

        return image;
    }
}