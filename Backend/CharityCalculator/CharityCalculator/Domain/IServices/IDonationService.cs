using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Domain.Models;

namespace CharityCalculator.Domain.IServices
{
    public interface IDonationService
    {
        /// <summary>
        /// Returns the current tax rate.
        /// </summary>
        /// <returns>Current tax rate</returns>
        Task<double> GetCurrentTaxRate();
        /// <summary>
        /// Updates the tax rate.
        /// </summary>
        /// <param name="amount">New value</param>
        /// <returns>Whether the update was successful</returns>
        Task<double> SetCurrentTaxRate(double amount);

        /// <summary>
        /// Calculates the deductible amount for the given donation.
        /// </summary>
        /// <param name="amount">Donation to calculate deductible amount of</param>
        /// <param name="eventType">Type of event</param>
        /// <returns>Deductible amount</returns>
        Task<double> GetDeductableAmount(double amount, string eventType);

        /// <summary>
        /// Fetches a list of all available event types
        /// </summary>
        /// <returns>List of event types</returns>
        Task<List<EventType>> GetEventTypes();

        /// <summary>
        /// Splits a donation into optimal pieces
        /// </summary>
        /// <param name="amount">Amount to split</param>
        /// <param name="eventType">Name of event type</param>
        /// <returns></returns>
        Task<List<double>> GetOptimalSplit(double amount, string eventType);

        /// <summary>
        /// Sets a new connection based on a provider connection string
        /// </summary>
        /// <param name="conn">Connection string</param>
        /// <returns>True if successful</returns>
        Task<bool> SetConnection(string conn);

        /// <summary>
        /// Sets a new connection based on a provider parameters
        /// </summary>
        /// <param name="database">Database name</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        Task<bool> SetConnection(string database, string username, string password);
    }
}
