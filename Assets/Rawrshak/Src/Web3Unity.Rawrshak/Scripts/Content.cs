using System;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Rawrshak
{
    public class Content
    {
        private static string AbiFileLocation = "Abis/Content";
        private static string abi = Resources.Load<TextAsset>(AbiFileLocation).text;

        public class RoyaltyResponse 
        { 
            public string receiver;
            public string rate; 
        }

        // ERC1155 API
        public static async Task<BigInteger> BalanceOf(string _chain, string _network, string _contract, string _account, string _tokenId, string _rpc="")
        {
            string method = "balanceOf";
            string[] obj = { _account, _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return BigInteger.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }

        public static async Task<List<BigInteger>> BalanceOfBatch(string _chain, string _network, string _contract, string[] _accounts, string[] _tokenIds, string _rpc="")
        {
            string method = "balanceOfBatch";
            string[][] obj = { _accounts, _tokenIds };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                string[] responses = JsonConvert.DeserializeObject<string[]>(response);
                List<BigInteger> balances = new List<BigInteger>();
                for (int i = 0; i < responses.Length; i++)
                {
                    balances.Add(BigInteger.Parse(responses[i]));
                }
                return balances;
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }        
        }

        public static async Task<bool> isApprovedForAll(string _chain, string _network, string _contract, string _account, string _operator, string _rpc="")
        {
            string method = "isApprovedForAll";
            string[] obj = { _account, _operator };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return Boolean.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }
        
        // Get the contract uri
        public static async Task<string> ContractUri(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "contractUri";
            string[] obj = {};
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get the contract royalty fee rate
        public static async Task<RoyaltyResponse> ContractRoyalty(string _chain, string _network, string _contract, string _rpc="")
        {
            string method = "contractRoyalty";
            string[] obj = {};
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            RoyaltyResponse royalties = JsonUtility.FromJson<RoyaltyResponse>(response);
            return royalties;
        }

        // Get the user mint nonce
        // Todo: Needs Content contract update
        public static async Task<string> UserMintNonce(string _chain, string _network, string _contract, string _address, string _rpc="")
        {
            string method = "userMintNonce";
            string[] obj = { _address };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get Most recent public token uri
        public static async Task<string> TokenUriWithVersion(string _chain, string _network, string _contract, string _tokenId, string _version, string _rpc="")
        {
            string method = "uri";
            string[] obj = { _tokenId, _version };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        public static async Task<string> TokenUri(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "uri";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            return response;
        }

        // Get the current total supply of an asset 
        public static async Task<BigInteger> TotalSupply(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "totalSupply";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return BigInteger.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }
        
        // Get the max supply of an asset 
        public static async Task<BigInteger> MaxSupply(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
        {
            string method = "maxSupply";
            string[] obj = { _tokenId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return BigInteger.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }

        // To Check for Interface implementation
        public static async Task<bool> SupportsInterface(string _chain, string _network, string _contract, string interfaceId, string _rpc="")
        {
            string method = "supportsInterface";
            string[] obj = { interfaceId };
            string args = JsonConvert.SerializeObject(obj);
            string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
            try 
            {
                return Boolean.Parse(response);
            } 
            catch 
            {
                Debug.LogError(response);
                throw;
            }
        }
        
        // Todo [Blocked]: ChainSafe's Gaming SDKs currently do not support non-WebGL transactions. We've 
        //                 requested this from the ChainSafe team but it is still in progress. In the 
        //                 meantime, we'll work with WalletConnect + Nethereum for sending transactions

        // public static async Task<string> SafeTransferFrom(string _chain, string _network, string _contract, string from, string to, string id, string amount, string _rpc="")
        // {
        //     string method = "safeTransferFrom";
        //     string[] obj = { from, to, id, amount, "" };
        //     string args = JsonConvert.SerializeObject(obj);
        //     string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        //     return response;
        // }

        // public static async Task<string> SafeBatchTransferFrom(string _chain, string _network, string _contract, SafeBatchTransferFromTransactionData transactionData, string _rpc="")
        // {
        //     string method = "safeBatchTransferFrom";
        //     string args = JsonConvert.SerializeObject(transactionData);
        //     string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        //     return response;
        // }

        // public static async Task<string> SetApprovedForAll(string _chain, string _network, string _contract, string oper, bool approved, string _rpc="")
        // {
        //     string method = "setApprovedForAll";
        //     string[] obj = { oper, approved ? "true" : "false" };
        //     string args = JsonConvert.SerializeObject(obj);
        //     string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        //     return response;
        // }

        // // Mint an array of assets; MintData must be signed by internal developer wallet
        // public static async Task<string> MintBatch(string _chain, string _network, string _contract, MintTransactionData data, string _rpc="")
        // {
        //     string method = "mintBatch";
        //     string args = JsonConvert.SerializeObject(data);
        //     string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        //     return response;
        // }

        // // Burn an array of assets; only user can burn
        // public static async Task<string> BurnBatch(string _chain, string _network, string _contract, BurnTransactionData data, string _rpc="")
        // {
        //     string method = "burnBatch";
        //     string args = JsonConvert.SerializeObject(data);
        //     string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        //     return response;
        // }
    }
}