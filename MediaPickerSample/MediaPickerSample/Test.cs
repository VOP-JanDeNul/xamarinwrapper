using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;

namespace MediaPickerSample
{
    public interface Test
    {
        ITesseractApi GetTesseractApi();
    }
}
