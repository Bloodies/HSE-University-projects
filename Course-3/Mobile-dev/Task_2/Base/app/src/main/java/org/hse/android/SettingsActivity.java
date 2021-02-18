package org.hse.android;

import android.Manifest;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.preference.PreferenceManager;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.FileProvider;
import com.bumptech.glide.Glide;

public class SettingsActivity extends AppCompatActivity implements SensorEventListener {

    private final static String TAG = "SettingsActivity";

    private static final int REQUEST_IMAGE_CAPTURE = 10;
    private static final int REQUEST_PERMISSION_CODE = 1;

    private final static String PERMISSION = Manifest.permission.CAMERA;

    private ImageView photo;
    private String imageFilePath;
    private EditText name;

    private PreferenceManager preferenceManager;
    private SensorManager sensorManager;
    private Sensor light;
    private TextView sensorLight;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        getSupportActionBar().hide();
        
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        light = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);

        View buttonPhoto = findViewById(R.id.button_photo);
        buttonPhoto.setOnClickListener(v -> checkPermission());

        View buttonSave = findViewById(R.id.button_save);
        buttonSave.setOnClickListener(v -> save());

        name = findViewById(R.id.name);
        photo = findViewById(R.id.photo);
        sensorLight = findViewById(R.id.sensor_light);

        initData();
    }

    public final void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    public final void onSensorChanged(SensorEvent event) {
        float lux = event.values[0];
        sensorLight.setText("{lux} lux");
    }

    @Override
    protected void onResume() {
        super.onResume();
        sensorManager.registerListener(this, light, SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onPause() {
        super.onPause();
        sensorManager.unregisterListener(this);
    }

    private void save() {
        preferenceManager.savePhotoValue(imageFilePath);
        Toast.makeText(this, "Данные были сохранены", Toast.LENGTH_SHORT).show();
    }

    private void initData() {
        imageFilePath = preferenceManager.getPhotoValue();
        loadPhoto();

        if(sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT) == null) {
            sensorLight.setText("Нет датчика освещенности");
        }
    }

    public void checkPermission() {
        int permissionsCheck = ActivityCompat.checkSelfPermission(this, PERMISSION);
        if (permissionsCheck != Package.PERMISSION_GRANTED) {
            if(ActivityCompat.shouldShowRequestPermissionRationale(this, PERMISSION)) {
                showExplanation("Нужно предоставить права",
                        "Для снятия фото нужно предоставить права на фото",
                        PERMISSION,
                        REQUEST_PERMISSION_CODE);
            }
            else {
                requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE);
            }
        }
        else {
            dispatchTakePictureIntent();
        }
    }

    private void showExplanation(String title, String message, final String permission, final int permissionRequestCode) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(title)
                .setMessage(message)
                .setPositiveButton(R.string.ok, new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        requestPermissions(permission, permissionRequestCode);
                    }
                });
        builder.create().show();
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
        if(resultCode == REQUEST_IMAGE_CAPTURE && resultCode == RESULT_OK) {
            loadPhoto();
            return;
        }
        super.onActivityResult(requestCode, resultCode, data);
    }

    private void loadPhoto() {
        if(imageFilePath != null) { Glide.with(this).load(imageFilePath).into(photo); }
    }

    private void dispatchTakePictureIntent() {
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        if(takePictureIntent.resolveActivity(getPackageManager()) != null) {
            File photoFile = null;
            try { photoFile = createImageFile(); }
            catch (IOException ex) { Log.e(TAG, "Create file", ex); }
            if(photoFile != null) {
                Uri photoURI = FileProvider.getUriForFile(this, BuildConfig.APPLICATION_ID + ".provider", photoFile);
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT,
                        photoURI);
                try { startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE); }
                catch (ActivityNotFoundException e) { Log.e(TAG, "Start activity", e); }
            }
        }
    }

    private File createImageFile() throws IOException {
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.getDefault()).format(new Date());
        String imageFileName = "IMG_" + timeStamp + "_";
        File storageDir = getExternalFilesDir(Environment.DIRECTORY_PICTURES);
        File image = File.createTempFile(
                imageFileName,  /* prefix */
                ".jpg",   /* suffix */
                storageDir      /* directory */
        );
        imageFilePath = image.getAbsolutePath();
        return image;
    }
}