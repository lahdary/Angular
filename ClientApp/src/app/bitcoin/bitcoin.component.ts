import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/RX';

@Component({
  selector: 'app-bitcoin',
  templateUrl: './bitcoin.component.html',
  styleUrls: ['./bitcoin.component.css']
})
export class BitcoinComponent implements OnInit {

  coinigy: any;


  title = 'app';
  btcUsd: number;
  timer: number = 30000; //milsec
  rate: number = 0.0058;   // %
  basisPrice: number;
  nominalBtc: number;
  nominalUsd: number;
  nominalInitialUsd: number = 100;
  init: boolean;
  btcUsdAtBuy: number;
  btcUsdAtStart: number;
  startTime: any;
  buy: boolean = true;
  equity: number;
  classic: number;
  exchangeCode: any = 'GDAX';
  exchangeMarket: any = 'BTC/USD';
  targetAsk: number;
  targetBid: number;

  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    this.init = true;
    this.setBasisPrice();
    this.start(this.basisPrice);
  }

  setBasisPrice() {
    // this.http.get('https://blockchain.info/ticker')
    // .subscribe(response =>{
    //   this.basisPrice = +JSON.stringify(response['USD']['last']);
    //   alert('Attention, Chargement des systèmes lunaires pour:' + this.basisPrice + '$, Dédicasse');
    // })

    var body = {
    }
    this.http.post('https://localhost:5001/api/bitcoin', body)
      .subscribe(
      (response: any) => {
        var content = JSON.parse(response.content);
        this.basisPrice = + content.USD;
        this.targetAsk = this.basisPrice + (this.rate * this.basisPrice);
        this.targetBid = this.basisPrice - (this.rate * this.basisPrice);
        console.log(`ticker: ${this.btcUsd}`);
      }
      )
    return this.btcUsd

  }
  getActualRate(): Observable<any> {
    let actualRate: number;
    return this.http.post('https://localhost:5001/api/bitcoin',{});
  }
  start(basisPrice: number) {
    this.startTime = Date.now();
    let timer = Observable.timer(0, this.timer);
    this.getActualRate()
      .subscribe((response) => {
        var content = JSON.parse(response.content);
        this.btcUsdAtStart = + content.USD;
        this.classic = this.btcUsdAtStart * this.nominalInitialUsd;

      })
    timer.subscribe(() => {
      let currentPrice = this.getTickerData();
      basisPrice = this.basisPrice;
      var ask = basisPrice + (this.rate * basisPrice);
      var bid = basisPrice - (this.rate * basisPrice);

      if (currentPrice > ask) {
        //Mise à jour dees prix
        this.basisPrice = currentPrice;
        this.targetAsk = currentPrice + (this.rate * currentPrice);
        this.targetBid = currentPrice - (this.rate * currentPrice);
        console.warn('TARGET Prices changed ask:'+this.targetAsk+' bid: '+this.targetBid);
        this.shiiit();
        if (this.buy) {
          this.buy = !this.buy;

          if (this.nominalUsd) {
            this.nominalBtc = this.nominalUsd / this.btcUsd;
            this.nominalUsd = 0;
          } else if (this.init && !this.nominalUsd) {
            this.btcUsdAtBuy = this.btcUsd;
            this.nominalBtc = this.nominalInitialUsd / this.btcUsd;
            this.nominalUsd = 0;
            this.init = false;
          }
          let warning = 'operation: buy $-->btc; ' + ' rate: ' + this.btcUsd + '  amount :' + this.nominalBtc + 'btc';

          console.warn(warning);

        }
      }
      if (currentPrice < bid) {
        //Mise à jour des prix
        this.basisPrice = currentPrice;
        this.targetAsk = currentPrice + (this.rate * currentPrice);
        this.targetBid = currentPrice - (this.rate * currentPrice);
        console.warn('TARGET Prices changed ask:'+this.targetAsk+' bid: '+this.targetBid);
        this.shiiit();
        if (!this.buy) {
          this.buy = !this.buy;

          if (this.nominalBtc) {
            this.nominalUsd = this.nominalBtc * this.btcUsd;
            this.nominalBtc = 0;
          }
          let warning = 'operation: sell btc-->$ ; ' + ' rate: ' + this.btcUsd + '  amount :' + this.nominalUsd + '$';
          console.warn(warning);
        }
      }

      //EQUITY
      if (this.nominalUsd != 0)this.equity = this.nominalUsd;
      if (this.nominalBtc != 0) this.equity =  this.nominalBtc * this.btcUsd;
      console.log('current:' + currentPrice);

    });
  }

  getTickerData() {
    var body = {
    }
    this.http.post('https://localhost:5001/api/bitcoin', body)
      .subscribe(
      (response: any) => {
        var content = JSON.parse(response.content);
        this.btcUsd = + content.USD;
      }
      )
    return this.btcUsd;
  }
shiiit(){
    let audio = new Audio();
    audio.src = "../assets/Shit.mp3";
    audio.load();
    audio.play();
  }
  

}
