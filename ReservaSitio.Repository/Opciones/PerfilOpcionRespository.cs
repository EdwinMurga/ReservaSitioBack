﻿using Dapper;
using Microsoft.Extensions.Configuration;
using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.DTOs;
using ReservaSitio.DTOs.Opciones;
using ReservaSitio.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static ReservaSitio.Entities.Enum;

namespace ReservaSitio.Repository.Opcion
{
   public  class PerfilOpcionRespository: BaseRepository, IPerfilOpcionRespository
    {

        private string _connectionString = "";
        private IConfiguration Configuration;
        public  PerfilOpcionRespository(ICustomConnection connection, IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_ReservaSitio");
        }


        public async Task<ResultDTO<PerfilOpcionDTO>> RegisterPerfilOpcion(PerfilOpcionDTO request)
            {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                        parameters.Add("@p_iid_perfil", request.iid_perfil);
                        parameters.Add("@p_iid_opcion", request.iid_opcion); 
                        parameters.Add("@p_iacceso_crear", request.iacceso_crear);
                        parameters.Add("@p_iacceso_actualizar", request.iacceso_actualizar);
                        parameters.Add("@p_iacceso_eliminar", request.iacceso_eliminar);
                        parameters.Add("@p_iacceso_visualizar", request.iacceso_visualizar);
                        parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                        parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);

                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PERFIL_OPCION_REGISTAR]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["id"].ToString());
                                res.IsSuccess = true;
                                res.Message = UtilMensajes.strInformnacionGrabada;
                            }
                        }
                        await mConnection.Complete();
                    }


                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();
                }
            }
            return res;
            }
        public async Task<ResultDTO<PerfilOpcionDTO>> DeletePerfilOpcion(PerfilOpcionDTO request)
            {
                ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    using (var cn = await mConnection.BeginConnection(true))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@p_vcodigo_cliente", request.iid_perfil_opcion);


                        using (var lector = await cn.ExecuteReaderAsync("[dbo].[]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                        {
                            while (lector.Read())
                            {
                                res.Codigo = Convert.ToInt32(lector["iid"].ToString());
                                res.IsSuccess = true;
                                res.Message = UtilMensajes.strInformnacionGrabada;
                            }
                        }
                        await mConnection.Complete();
                    }


                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    res.IsSuccess = false;
                    res.Message = UtilMensajes.strInformnacionNoGrabada;
                    res.InnerException = e.Message.ToString();
                }
            }
            return res;
            }
        public async Task<ResultDTO<PerfilOpcionDTO>> GetPerfilOpcion(PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            PerfilOpcionDTO item = new PerfilOpcionDTO();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                using (var cn = new SqlConnection(_connectionString))
                {

                    var query = await cn.QueryAsync<PerfilOpcionDTO>("[dbo].[SP_PERFIL_OPCION_BY_ID]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    item = (PerfilOpcionDTO)query.FirstOrDefault();
                    res.IsSuccess = (query.Any() == true ? true : false);
                }
                // await mConnection.Complete();
                res.Message = (res.IsSuccess ? UtilMensajes.strInformnacionGrabada : UtilMensajes.strInformnacionNoEncontrada);
                res.item = item;
            }
            catch (Exception e)
            {

                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }
        public async Task<ResultDTO<PerfilOpcionDTO>> GetListPerfilOpcion(PerfilOpcionDTO request)
        {
            ResultDTO<PerfilOpcionDTO> res = new ResultDTO<PerfilOpcionDTO>();
            List<PerfilOpcionDTO> list = new List<PerfilOpcionDTO>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_perfil_opcion", request.iid_perfil_opcion);
                parameters.Add("@p_iid_perfil", request.iid_perfil);
                parameters.Add("@p_iid_opcion", request.iid_opcion);
                parameters.Add("@p_iid_estado_registro", request.iid_estado_registro);
                parameters.Add("@p_iid_usuario_registra", request.iid_usuario_registra);
                parameters.Add("@p_indice", request.pageNum);
                parameters.Add("@p_limit", request.pageSize);

                using (var cn = new SqlConnection(_connectionString))
                {

                    list = (List<PerfilOpcionDTO>)cn.Query<PerfilOpcionDTO>("[dbo].[SP_PERFIL_OPCION_LISTAR]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = (list.ToList().Count > 0 ? true : false);
                res.totalregistro = (int)(list.ToList().Count > 0 ? list[0].totalRecord : 0);
                res.Message = (list.ToList().Count > 0 ? UtilMensajes.strInformnacionEncontrada : UtilMensajes.strInformnacionNoEncontrada);
                res.data = list.ToList();
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = UtilMensajes.strInformnacionNoGrabada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }


    }


}
