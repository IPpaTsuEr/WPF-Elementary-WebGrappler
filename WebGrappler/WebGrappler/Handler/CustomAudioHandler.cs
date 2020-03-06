using CefSharp;
using CefSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.Handler
{
    class CustomAudioHandler : IAudioHandler
    {
        public void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, int audioStreamId, IntPtr data, int noOfFrames, long pts)
        {
            Console.WriteLine("OnAudioStreamPacket");
        }

        public void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, int audioStreamId, int channels, ChannelLayout channelLayout, int sampleRate, int framesPerBuffer)
        {
            Console.WriteLine("OnAudioStreamStarted");
        }

        public void OnAudioStreamStopped(IWebBrowser chromiumWebBrowser, IBrowser browser, int audioStreamId)
        {
            Console.WriteLine("OnAudioStreamStopped");
        }
    }
}
