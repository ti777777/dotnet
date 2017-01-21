﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Profiling.Storage
{
    /// <summary>
    /// How lists should be sorted.
    /// </summary>
    public enum ListResultsOrder
    {
        /// <summary>
        /// Ascending Order
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending Order
        /// </summary>
        Descending
    }

    /// <summary>
    /// Provides saving and loading <see cref="MiniProfiler"/>s to a storage medium.
    /// </summary>
    public interface IAsyncStorage
    {
        /// <summary>
        /// List the latest profiling results.
        /// </summary>
        IEnumerable<Guid> List(
            int maxResults,
            DateTime? start = null,
            DateTime? finish = null,
            ListResultsOrder orderBy = ListResultsOrder.Descending);

        /// <summary>
        /// Stores <paramref name="profiler"/> under its <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <param name="profiler">The results of a profiling session.</param>
        /// <remarks>
        /// Should also ensure the profiler is stored as being un-viewed by its profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        void Save(MiniProfiler profiler);

        /// <summary>
        /// Returns a <see cref="MiniProfiler"/> from storage based on <paramref name="id"/>, 
        /// which should map to <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <remarks>
        /// Should also update that the resulting profiler has been marked as viewed by its 
        /// profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        MiniProfiler Load(Guid id);

        /// <summary>
        /// Sets a particular profiler session so it is considered "un-viewed"  
        /// </summary>
        void SetUnviewed(string user, Guid id);

        /// <summary>
        /// Sets a particular profiler session to "viewed"
        /// </summary>
        void SetViewed(string user, Guid id);

        /// <summary>
        /// Returns a list of <see cref="MiniProfiler.Id"/>s that haven't been seen by <paramref name="user"/>.
        /// </summary>
        /// <param name="user">User identified by the current <c>MiniProfiler.Settings.UserProvider</c></param>
        List<Guid> GetUnviewedIds(string user);

        /// <summary>
        /// Asynchronously list the latest profiling results.
        /// </summary>
        Task<IEnumerable<Guid>> ListAsync(
            int maxResults,
            DateTime? start = null,
            DateTime? finish = null,
            ListResultsOrder orderBy = ListResultsOrder.Descending);

        /// <summary>
        /// Asynchronously stores <paramref name="profiler"/> under its <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <param name="profiler">The results of a profiling session.</param>
        /// <remarks>
        /// Should also ensure the profiler is stored as being un-viewed by its profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        Task SaveAsync(MiniProfiler profiler);

        /// <summary>
        /// Asynchronously returns a <see cref="MiniProfiler"/> from storage based on <paramref name="id"/>, 
        /// which should map to <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <remarks>
        /// Should also update that the resulting profiler has been marked as viewed by its 
        /// profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        Task<MiniProfiler> LoadAsync(Guid id);

        /// <summary>
        /// Asynchronously sets a particular profiler session so it is considered "un-viewed"  
        /// </summary>
        Task SetUnviewedAsync(string user, Guid id);

        /// <summary>
        /// Asynchronously sets a particular profiler session to "viewed"
        /// </summary>
        Task SetViewedAsync(string user, Guid id);

        /// <summary>
        /// Asynchronously returns a list of <see cref="MiniProfiler.Id"/>s that haven't been seen by <paramref name="user"/>.
        /// </summary>
        /// <param name="user">User identified by the current <c>MiniProfiler.Settings.UserProvider</c></param>
        Task<List<Guid>> GetUnviewedIdsAsync(string user);
    }
}
