using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Threading.Tasks;
using System.Net.Http;

namespace AsyncTaskPractice
{
    [Activity(Label = "AsyncTaskPractice", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonStart;
        private ProgressDialog _progressDialog;
        private TextView _textViewOutput;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _textViewOutput = FindViewById<TextView>(Resource.Id.activity_main_text_view_output);
            _buttonStart = FindViewById<Button>(Resource.Id.activity_main_button_start);


            _buttonStart.Click += async (sender, e) =>
            {
                Task<string> clientTask = MyTask();

                _runDialog();

                var result = await clientTask;

                _progressDialog.Hide();

                _textViewOutput.Text = result;
            };

        }

        private void _runDialog()
        {
            _progressDialog = new ProgressDialog(this);
            _progressDialog.SetTitle("Progress Dialog");
            _progressDialog.SetMessage("Downloading...");
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.Show();

        }
        public async Task<string> MyTask()
        {
            HttpClient client = new HttpClient();
            Task<string> contentsTask = client.GetStringAsync("http://www.fakeresponse.com/api/?data={%22answer%22:%2042}&meta=false&sleep=3");
            string contents = await contentsTask;


            return contents;
        }
    }
}

