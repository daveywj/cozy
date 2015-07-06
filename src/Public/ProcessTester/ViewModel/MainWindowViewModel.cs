﻿using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using CozyAnywhere.Plugin.WinProcess;
using CozyAnywhere.Plugin.WinProcess.Model;

namespace ProcessTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<WinProcess> _ProcessInfoList = new ObservableCollection<WinProcess>();

        public ObservableCollection<WinProcess> ProcessInfoList
        {
            get
            {
                return _ProcessInfoList;
            }
            set
            {
                Set(ref _ProcessInfoList, value, "ProcessInfoList");
            }
        }

        public MainWindowViewModel()
        {
            TestData();
        }

        private void TestData()
        {
            ProcessUtil.ProcessEnum((pid) =>
            {
                string result = null;
                ProcessUtil.GetProcessName(pid, (x) =>
                {
                    result = Marshal.PtrToStringAuto(x);
                });
                if (result != null)
                {
                    ProcessInfoList.Add(
                    new WinProcess
                    {
                        Name        = result,
                        ProcessId   = pid,
                    }
                    );
                }
                return false;
            });
        }
    }
}