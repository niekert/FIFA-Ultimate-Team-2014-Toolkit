﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Factories;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace UltimateTeam.Toolkit
{
    public class FutClient : IFutClient
    {
        private readonly FutRequestFactories _requestFactories;

        public FutClient()
            : this(new FutRequestFactories())
        {
        }

        public FutClient(FutRequestFactories requestFactories)
        {
            requestFactories.ThrowIfNullArgument();
            _requestFactories = requestFactories;
        }

        public async Task<LoginResponse> LoginAsync(LoginDetails loginDetails)
        {
            loginDetails.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.LoginRequestFactory(loginDetails).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Login failed", e);
            }
        }

        public async Task<AuctionResponse> SearchAsync(SearchParameters searchParameters)
        {
            searchParameters.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Search failed", e);
            }
        }

        public async Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            try
            {
                return await _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Placing bid failed", e);
            }
        }

        public async Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.ItemRequestFactory(auctionInfo).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get item failed", e);
            }
        }

        public async Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get player image failed", e);
            }
        }

        public async Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            try
            {
                return await _requestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync();
            }
            catch (Exception e)
            {
                throw new FutException("Get trade statuses failed", e);
            }
        }
    }
}
