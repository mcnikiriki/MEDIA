﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Media.src;
using Media.src.objects;
using MEDIA.src;
using MEDIA.src.objects;

namespace MEDIA
{
    class Program
    {
        public static async Task Main()
        {
            // await CreateProjectStructure();
            await RecognitionVideos();
        }

        private static async Task DownloadVideo()
        {
            List<string> videos = new List<string>() { "https://www.youtube.com/shorts/ZPiYFU4pqiQ" };
            string downloadPath = "/home/mcniki/Видео/";

            IDownloadContentService downloadVideo = new DownloadVideoService(videos, downloadPath);
            await downloadVideo.DownloadContents();
        }

        private static async Task EditVideo()
        {
            List<string> videosPath = new List<string>() {"/home/mcniki/Видео/video.webm"};
            string timePartVideo = "00:00:30";

            IEditingContentService editingVideo = new EditingVideoService(videosPath, timePartVideo);
            await editingVideo.CutContents();
        }

        private static async Task EditAudio()
        {
            List<string> audiosPath = new List<string>() {"/home/mcniki/Видео/audio.mp3"};
            string timePartAudio = "00:00:15";

            IEditingContentService editingAudio = new EditingAudioService(audiosPath, timePartAudio);
            await editingAudio.CutContents();
        }

        private static async Task DownloadAudio()
        {
            List<string> audios = new List<string>() { "https://www.youtube.com/shorts/ZPiYFU4pqiQ" };
            string downloadPath = "/home/mcniki/Видео/";

            IDownloadContentService downloadAudio = new DownloadAudioService(audios, downloadPath);
            await downloadAudio.DownloadContents();
        }

        private static async Task DownloadThumbnail()
        {
            List<string> videoUrls = new List<string>() { "https://www.youtube.com/shorts/ZPiYFU4pqiQ" };
            string downloadPath = "/home/mcniki/Изображения/";

            IDownloadContentService downloadThumbnail = new DownloadThumbnailService(videoUrls, downloadPath);
            await downloadThumbnail.DownloadContents();
        }

        private static async Task DownloadVideoInfo()
        {
            List<string> videoUrls = new List<string>() { "https://www.youtube.com/shorts/ZPiYFU4pqiQ" };
            string downloadPath = "/home/mcniki/Документы/";

            IDownloadContentService downloadVideoInfo = new DownloadVideoInfoService(videoUrls, downloadPath);
            await downloadVideoInfo.DownloadContents();
        }

        private static async Task CreateProjectStructure()
        {
            string baseDirectory = "/home/mcniki/Видео/";
            List<string> videoUrls = new List<string> { "https://www.youtube.com/shorts/Gno-zBED01Y" };
            string cutDuration = "00:00:10";

            var projectManager = new ProjectStructureManager(baseDirectory, videoUrls, cutDuration);
            await projectManager.CreateProjectStructure();
        }

        private static async Task RecognitionVideos()
        {
            string videoPath = "/home/mcniki/Видео/video/";
            string modelPath = "/home/mcniki/Документы/Code/C#/Media/Media/models/whisper-small"; // Укажите путь к модели Vosk

            ISpeechRecognitionService speechRecognitionService = new SpeechRecognitionService(videoPath, modelPath);
            await speechRecognitionService.RecognizeVideos();
        }
    }
}