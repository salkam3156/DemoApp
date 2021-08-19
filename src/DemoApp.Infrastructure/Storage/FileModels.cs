using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Models
{
    public sealed class TransientFileModel : IAsyncDisposable, IDisposable
    {
        private bool disposedValue;

        public Lazy<FileStream> FileStream { get; private init; }

        public TransientFileModel(FileLocation location)
        {
            // get protocol based on Uri etc
            // provision storage location / space
            // return file bound for deletion after stream read
            // and so on.
            FileStream = new Lazy<FileStream>(
                () => new FileStream(location.Location.AbsoluteUri,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None,
                bufferSize: 4096,
                FileOptions.SequentialScan | FileOptions.DeleteOnClose | FileOptions.Asynchronous));
        }

        public async ValueTask DisposeAsync() // would normally implement disposeasynccore
        {
            if (FileStream.IsValueCreated)
            {
                await FileStream.Value.FlushAsync();
                await FileStream.Value.DisposeAsync();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (FileStream.IsValueCreated)
                    { 
                        FileStream.Value?.Flush();
                        FileStream.Value?.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public sealed class FileLocation
    {
        public Uri Location { get; private init; }
    }
}
