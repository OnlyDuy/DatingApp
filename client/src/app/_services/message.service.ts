import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})

// Đưa http vào đây
export class MessageService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // phương thức nhận tin nhắn
  getMessages(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'message', params, this.http);
  }

  getMessageThread(username: string) {
    return this.http.get<Message[]>(this.baseUrl + 'message/thread/' + username);
  }

  deleteMessage(id: number) {
    return this.http.delete(this.baseUrl + 'message/' + id);
  }

  sendMessage(username: string, content: string) {
    return this.http.post<Message>(this.baseUrl + 'message', {recipientUsername: username, content})
  }
}