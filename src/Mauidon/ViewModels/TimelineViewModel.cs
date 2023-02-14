// <copyright file="TimelineViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Mastonet;
using Mastonet.Entities;
using Mauidon.Models;

namespace Mauidon.ViewModels
{
    public abstract class TimelineViewModel : MauidonBaseViewModel, IDisposable
    {
        private bool disposedValue;
        private TimelineStreaming streaming;
        private MastodonTimelineType timelineType;

        public TimelineViewModel(MastodonTimelineType timelineType, IServiceProvider services)
            : base(services)
        {
            this.timelineType = timelineType;
            this.Statuses = new ObservableCollection<Status>();
            this.streaming = timelineType switch
            {
                MastodonTimelineType.Public => this.MauidonClient.Client.GetPublicStreaming(),
                _ => throw new NotImplementedException(),
            };
            this.streaming.OnUpdate += this.StreamingOnUpdate;
            this.streaming.OnDelete += this.StreamingOnDelete;
            this.streaming.OnConversation += this.StreamingOnConversation;
            this.streaming.OnNotification += this.StreamingOnNotification;
            this.streaming.OnFiltersChanged += this.StreamingOnFiltersChanged;

            this.StartStreaming();
        }

        public ObservableCollection<Status> Statuses { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Start Streaming.
        /// </summary>
        public void StartStreaming()
            => this.streaming.Start();

        /// <summary>
        /// Stop Streaming.
        /// </summary>
        public void StopStreaming()
           => this.streaming.Stop();

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Is Disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.streaming is not null)
                    {
                        this.streaming.Stop();
                        this.streaming.OnUpdate -= this.StreamingOnUpdate;
                        this.streaming.OnDelete -= this.StreamingOnDelete;
                        this.streaming.OnConversation -= this.StreamingOnConversation;
                        this.streaming.OnNotification -= this.StreamingOnNotification;
                        this.streaming.OnFiltersChanged -= this.StreamingOnFiltersChanged;
                    }
                }

                this.disposedValue = true;
            }
        }

        private void StreamingOnFiltersChanged(object? sender, StreamFiltersChangedEventArgs e)
        {
        }

        private void StreamingOnNotification(object? sender, StreamNotificationEventArgs e)
        {
        }

        private void StreamingOnConversation(object? sender, StreamConversationEvenTargs e)
        {
        }

        private void StreamingOnDelete(object? sender, StreamDeleteEventArgs e)
        {
            if (this.Statuses.FirstOrDefault(n => n.Id == e.StatusId.ToString()) is Status status)
            {
                this.Statuses.Remove(status);
            }
        }

        private void StreamingOnUpdate(object? sender, StreamUpdateEventArgs e)
        {
            // This is wrong, it should be inserted by index. But the status id is a... string?
            this.Statuses.Insert(0, e.Status);
        }
    }
}
