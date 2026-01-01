using System;
using System.Collections.Generic;
using ZXing;
using ZXing.Common;
using CommunityToolkit.Mvvm.Input;

namespace QRCoder_Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private string _inputText = "Hello HiSeatown";
        private BitMatrix? _barcodeMatrix;

        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        public BitMatrix? BarcodeMatrix
        {
            get => _barcodeMatrix;
            private set => SetProperty(ref _barcodeMatrix, value);
        }

        public IRelayCommand GenerateCommand { get; }

        public MainWindowViewModel()
        {
            GenerateCommand = new RelayCommand(Generate);
        }

        private void Generate()
        {
            try
            {
                var writer = new MultiFormatWriter();
                var hints = new Dictionary<EncodeHintType, object>
                {
                    { EncodeHintType.MARGIN, 1 }
                };

                int width = 400;
                int height = 200;

                BarcodeMatrix = writer.encode(InputText ?? string.Empty, BarcodeFormat.CODE_128, width, height, hints);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}